using ListModels;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// オンラインが関わるメソッドをまとめたクラス
/// </summary>
public class OnlineMethod : MonoBehaviourPun
{
    bool _isOnline;
    Field_mgr _fieldManager;
    Battle_mgr _battleManager;

    private void Awake()
    {
        _isOnline = MyPlayer.Instance.IsOnline; // 選択モードによって切り替わる

        _fieldManager = this.GetComponent<Field_mgr>();
        _battleManager = this.GetComponent<Battle_mgr>();

        _fieldManager.ToggleFunc += Toggle;
        _fieldManager.FillHandAction += FillHand;

        _battleManager.SetEnemyNameAction += this.SetEnemyNameText;
        _battleManager.FillHands += this.FillAllHands;
        _battleManager.EnemyAction += SetEnemyAction;
        _battleManager.TransitionEvent += TransitionAll;
        _battleManager.CoinToss += DecideAtkPlayer;
    }

    public void SetEnemyNameText()
    {
        if (_isOnline) // オンライン
        {
            string enemyName = PunManager.Instance.GetEnemyName();
            SetEnemyName(enemyName);
        }
        else //オフライン
        {
            SetEnemyName("CPU");
        }
    }

    public void SetEnemyName(string name)
    {
        _battleManager.SetEnemyName(name);
    }

    public void SetEnemyAction()
    {
        if (_isOnline) // オンライン
        {
            // 相手側で SetPlayerAction() を呼ぶ
            photonView.RPC("RPC_SetPlayerAction", RpcTarget.Others);
        }
        else // オフライン
        {
            _battleManager.CPUAction();
        }
    }

    [PunRPC]
    public void RPC_SetPlayerAction()
    {
        _battleManager.SetAction(true); // 相手側の操作を有効化
    }

    public void FillHand(bool isMine)
    {
        Hand_mdl hand = _fieldManager.GetMyHand(isMine);
        Card_mdl card;

        while (hand.Count < _fieldManager.MaxHandSize)
        {
            card = _fieldManager.Pop(isMine);
            if (card != null) _fieldManager.Draw(card, isMine);

            if (_isOnline && isMine) // オンライン 各プレイヤーが自分のisMine=trueで呼ぶ
            {
                // 相手側の
                // OnlineMethod.Draw(card, false);
                photonView.RPC("RPC_Draw", RpcTarget.Others, card.ID, false);
            }
        }
        _fieldManager.ShowHand(hand, isMine);
    }

    [PunRPC]
    public void RPC_Draw(int cardID, bool isMine)
    {
        Card_mdl card = new Card_mdl(cardID);
        _fieldManager.Draw(card, isMine);

        Hand_mdl hand = _fieldManager.GetMyHand(isMine);
        _fieldManager.ShowHand(hand, isMine);
    }

    public void FillAllHands()
    {
        if (_isOnline) // オンライン
        {
            // お互いのFillHandを同時に呼ぶみたいな
            // 片側(Host的な)だけで行われる操作
            // MasterClient（Host）だけが両者の手札補充を起動する
            Debug.Log($"FillAllHands: IsMasterClient={PhotonNetwork.IsMasterClient}");
            //if (PhotonNetwork.IsMasterClient) // 消したくないが，消さないと手札補充などのタイミングと同期タイミングのずれによりクライアントの防御開始時の手札が補充されないためバグが起きない限り消しておく．
            //{
            _fieldManager.FillHand(true);  // 自分（Host）の手札
                photonView.RPC("RPC_FillHand", RpcTarget.Others); // Client側の手札
            //}
            _fieldManager.ShowHand(_fieldManager.GetMyHand(true), true);
            _fieldManager.ShowHand(_fieldManager.GetMyHand(false), false);
        }
        else // オフライン
        {
            _fieldManager.FillHand(true); // 自分の手札を満たす
            _fieldManager.FillHand(false); // CPUの手札を満たす
        }
    }

    [PunRPC]
    public void RPC_FillHand()
    {
        Debug.Log("RPC_FillHand: received");
        _fieldManager.FillHand(true); // 受け取った側が自分の手札を補充
    }

    public void FillPlayerHand()
    {
        _fieldManager.FillHand(true);
    }

    public void TransitionAll()
    {
        Debug.Log($"TransitionAll called: IsMasterClient={PhotonNetwork.IsMasterClient}, IsOnline={_isOnline}");
        if (_isOnline) // オンライン
        {
            // 両側の
            // OnlineMethod.Transition();
            // を呼ぶ．片側だけで行われる（全体で１回）
            //if (PhotonNetwork.IsMasterClient) // Host側だけ起動（全体で1回）
            //{
            Debug.Log("Sending RPC_Transition");
            photonView.RPC("RPC_Transition", RpcTarget.All);
            //}
        }
        else // オフライン
        {
            Transition();
        }
    }

    [PunRPC]
    public void RPC_Transition()
    {
        Debug.Log("RPC_Transition received");
        _battleManager.Transition();
    }

    public void Transition()
    {
        _battleManager.Transition();
    }

    public void DecideAtkPlayer()
    {
        if (_isOnline)
        {
            if (PhotonNetwork.IsMasterClient) // Host側だけコイントスする
            {
                bool isMyAtk = CoinToss();
                SetAttacker(isMyAtk);
                // 相手には逆の結果を送る
                photonView.RPC("RPC_SetAttacker", RpcTarget.Others, !isMyAtk);
            }
        }
        else
        {
            bool isMyAtk = CoinToss();
            SetAttacker(isMyAtk);
        }
    }

    [PunRPC]
    public void RPC_SetAttacker(bool isMyAtk)
    {
        SetAttacker(isMyAtk);
    }

    public void SetAttacker(bool isMyAtk)
    {
        _battleManager.IsMyAtk = isMyAtk;
        _battleManager.SetPhase(Battle_mgr.GamePhase.SelectAtk);
    }

    bool CoinToss()
    {
        int coin = UnityEngine.Random.Range(0, 2);
        return coin == 0 ? true : false;
    }

    public bool Toggle(Card_mdl card, bool isMine)
    {
        if (_fieldManager.TypeCheck(card, _fieldManager.GetMyArea(isMine))) return false;

        _fieldManager.SubToggle(card, isMine);

        if (_isOnline) // オンライン
        {
            // 相手側の
            // OnlineMethod.SubToggle(card, false);
            // を呼んで，相手側目線の敵側エリアに選んだカードをトグル
            photonView.RPC("RPC_SubToggle", RpcTarget.Others, card.ID, false);
        }
        return true;
    }

    [PunRPC]
    public void RPC_SubToggle(int cardID, bool isMine)
    {
        Card_mdl card = new Card_mdl(cardID);
        _fieldManager.SubToggle(card, isMine);
    }

    public void SubToggle(Card_mdl card, bool isMine)
    {
        _fieldManager.SubToggle(card, isMine);
    }
}
