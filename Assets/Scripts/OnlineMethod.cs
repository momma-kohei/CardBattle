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
            // 相手側の
            // OnlineMethod.SetPlayerAction();
        }
        else // オフライン
        {
            _battleManager.CPUAction();
        }
    }

    public void SetPlayerAction()
    {
        _battleManager.SetAction(true);
    }

    public void FillHand(bool isMine)
    {
        Hand_mdl hand = _fieldManager.GetMyHand(isMine);
        Card_mdl card;

        while (hand.Count < _fieldManager.MaxHandSize)
        {
            card = _fieldManager.Pop(isMine);
            if (card != null) _fieldManager.Draw(card, isMine);

            if (_isOnline && isMine) // オンライン
            {
                // 相手側の
                // OnlineMethod.Draw(card, false);
            }
        }
        _fieldManager.ShowHand(hand, isMine);
    }

    public void Draw(Card_mdl card, bool isMine)
    {
        _fieldManager.Draw(card, isMine);
    }

    public void FillAllHands()
    {
        if (_isOnline) // オンライン
        {
            // お互いのFillHandを同時に呼ぶみたいな
            // 片側(Host的な)だけで行われる操作
            // 両側の
            // OnlineMethod.FillPlayerHand();
        }
        else // オフライン
        {
            _fieldManager.FillHand(true); // 自分の手札を満たす
            _fieldManager.FillHand(false); // CPUの手札を満たす
        }
    }

    public void FillPlayerHand()
    {
        _fieldManager.FillHand(true);
    }

    public void TransitionAll()
    {
        if (_isOnline) // オンライン
        {
            // 両側の
            // OnlineMethod.Transition();
            // を呼ぶ．片側だけで行われる（全体で１回）
        }
        else // オフライン
        {
            Transition();
        }
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
