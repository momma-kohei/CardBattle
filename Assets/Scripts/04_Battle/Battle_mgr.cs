using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルに関する処理を行うクラス
/// </summary>
public class Battle_mgr : MonoBehaviour
{
    [SerializeField] Battle_ctl _battleC;
    [SerializeField] Field_mgr _fieldM;
    [SerializeField] Field_viw _fieldV;
    [SerializeField] CPU_mgr _cpuM;
    [SerializeField] Button _centerButton;

    [SerializeField] int _maxHitPoint = 30;

    public Action OnCenterButtonClicked; // センターボタンがクリックされたときのイベント

    AudioSource _buttonSound;
    int _hitPoint1;
    int _hitPoint2;

    public int HitPoint1 { get => _hitPoint1; set => _hitPoint1 = value; }
    public int HitPoint2 { get => _hitPoint2; set => _hitPoint2 = value; }

    GamePhase _currentPhase;

    bool _logOnce = false; // デバッグ用のフラグ


    void Start()
    {
        _hitPoint1 = _maxHitPoint;
        _hitPoint2 = _maxHitPoint;

        _fieldM.FillHand1(); // 自分の手札を満たす

        // オフライン
        _cpuM.FillHand2(); // CPUの手札を満たす

        SetPhase(GamePhase.PlayerSelect1);

        // オンラインの場合は、立場の弱い方（クライアント）がここでSetPhase(GamePhase.EnemySelect1)とか
    }

    void Update()
    {
        if (_currentPhase == GamePhase.Finish && !_logOnce)
        {
            if (_hitPoint1 <= 0)
            {
                Debug.Log("Player2の勝利！");
                _logOnce = true;
            }
            else if (_hitPoint2 <= 0)
            {
                Debug.Log("Player1の勝利！");
                _logOnce = true;
            }
        }
        else if (_hitPoint1 <= 0 || _hitPoint2 <= 0)
        {
            SetPhase(GamePhase.Finish);
        }
    }

    public void ResetField()
    {
        _fieldM.Trash(); // 場札をすべて捨てる
        Invoke(nameof(FillHands), 1f); // 手札を満たす処理を少し遅らせて呼び出す
    }

    void FillHands()
    {
        _fieldM.FillHand1(); // 自分の手札を満たす

        // オフライン
        _cpuM.FillHand2(); // CPUの手札を満たす

        // オンライン
        // 相手プレイヤーの手札を満たす処理
    }

    public void Battle1() // player1が攻撃側の場合のバトル処理
    {
        _fieldM.OpenArea(); // 場札を公開

        _hitPoint2 -= _battleC.CalcDamage(true); // 相手のHPからダメージを引く
        Debug.Log($"Player1の攻撃！ Player2の残りHP: {_hitPoint2}");
        Invoke(nameof(ResetField), 1f);
    }

    public void Battle2()
    {
        _fieldM.OpenArea(); // 場札を公開

        _hitPoint1 -= _battleC.CalcDamage(false); // 自分ののHPからダメージを引く
        Debug.Log($"Player2の攻撃！ Player1の残りHP: {_hitPoint1}");
        Invoke(nameof(ResetField), 1f);
    }

    public void SetPhase(GamePhase nextPhase)
    {
        _currentPhase = nextPhase;

        switch (nextPhase)
        {
            case GamePhase.PlayerSelect1:
                Debug.Log("PlayerSelect1フェーズに遷移");

                // 自分のクリックを有効化
                SetP1CardAction(); // PlayerSelect1のカードにクリックイベントを登録
                break;

            case GamePhase.EnemySelect1:
                Debug.Log("EnemySelect1フェーズに遷移");

                UnsetP1CardAction(); // PlayerSelect1のカードのクリックイベントを解除
                UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除

                // オフライン
                _cpuM.CallActionCPU(); // CPUの行動を呼び出す
                Invoke(nameof(Transition), 1f); // CPUの行動が終わるまでの時間を設定してTransitionを呼び出す

                // オンライン
                // 相手のクリックを有効化
                // 自分は待機
                break;

            case GamePhase.Battle1:
                Debug.Log("Battle1フェーズに遷移 - Player1の攻撃処理を実行");

                UnsetP1CardAction(); // PlayerSelect1のカードのクリックイベントを解除
                UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除

                // 数値処理やアニメーションなどのバトル処理を実行
                Battle1();

                Invoke(nameof(Transition), 3f); // バトル処理が終わるまでの時間を設定してTransitionを呼び出す
                break;

            case GamePhase.EnemySelect2:
                Debug.Log("EnemySelect2フェーズに遷移");

                UnsetP1CardAction(); // PlayerSelect1のカードのクリックイベントを解除
                UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除

                // オフライン
                _cpuM.CallActionCPU(); // CPUの行動を呼び出す
                Invoke(nameof(Transition), 1f); // CPUの行動が終わるまでの時間を設定してTransitionを呼び出す


                // オンライン
                // 相手のクリックを有効化
                // 自分は待機
                break;

            case GamePhase.PlayerSelect2:
                Debug.Log("PlayerSelect2フェーズに遷移");

                // 自分のクリックを有効化
                SetP1CardAction(); // PlayerSelect1のカードにクリックイベントを登録
                break;

            case GamePhase.Battle2:
                Debug.Log("Battle2フェーズに遷移 - Player2の攻撃処理を実行");

                UnsetP1CardAction(); // PlayerSelect1のカードのクリックイベントを解除
                UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除

                // 数値処理やアニメーションなどのバトル処理を実行
                Battle2();

                Invoke(nameof(Transition), 3f); // バトル処理が終わるまでの時間を設定してTransitionを呼び出す
                break;

            case GamePhase.Finish:
                Debug.Log("Finishフェーズに遷移 - ゲーム終了");

                UnsetP1CardAction(); // PlayerSelect1のカードのクリックイベントを解除
                UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除

                // ゲーム終了
                break;
        }
    }

    public void Transition()
    {
        switch (_currentPhase)
        {
            case GamePhase.PlayerSelect1:
                SetPhase(GamePhase.EnemySelect1);
                break;
            case GamePhase.EnemySelect1:
                SetPhase(GamePhase.Battle1);
                break;
            case GamePhase.PlayerSelect2:
                SetPhase(GamePhase.Battle2);
                break;
            case GamePhase.EnemySelect2:
                SetPhase(GamePhase.PlayerSelect2);
                break;
            case GamePhase.Battle1:
                SetPhase(GamePhase.EnemySelect2);
                break;
            case GamePhase.Battle2:
                SetPhase(GamePhase.PlayerSelect1);
                break;
        }
        // 自分のセレクト終了時に自分と相手のTransitionを呼び出す
    }

    //public void Finish()
    //{
    //    if (_hitPoint1 <= 0 || _hitPoint2 <= 0) SetPhase(GamePhase.Finish);
    //}

    public enum GamePhase
    {
        PlayerSelect1,
        PlayerSelect2,
        EnemySelect1,
        EnemySelect2,
        Battle1,
        Battle2,
        Finish
    }

    public void SetP1CardAction()
    {
        foreach (Card_viw cardV in _fieldV.GetHand1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked += OnP1CardClicked; // Hand1のカードにクリックイベントを登録
        }
        foreach (Card_viw cardV in _fieldV.GetArea1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked += OnP1CardClicked; // Area1のカードにクリックイベントを登録
        }
    }

    public void UnsetP1CardAction()
    {
        foreach (Card_viw cardV in _fieldV.GetHand1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked -= OnP1CardClicked; // Hand1のカードのクリックイベントを解除
        }
        foreach (Card_viw cardV in _fieldV.GetArea1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked -= OnP1CardClicked; // Area1のカードのクリックイベントを解除
        }
    }

    void OnP1CardClicked(Card_viw card)
    {
        // toggle & Area表示
        if (_fieldM.Toggle(card.GetModel()))
        {
            // Hand移動
            card.MoveCard();
            // 効果音再生
            card.PlaySound();

            UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除
            if (!_fieldM.IsAreaEmpty())
            {
                SetCenterButtonAction(); // センターボタンのクリックイベントを登録
            }
        }
        // Power表示
    }

    
    // センターボタンに関する処理
    public void OnCenterButtonClick()
    {
        OnCenterButtonClicked?.Invoke(); // センターボタンがクリックされたときのイベントを呼び出す
    }

    void SetCenterButtonAction()
    {
        OnCenterButtonClicked += CenterButtonEvent; // センターボタンにクリックイベントを登録
    }

    void UnsetCenterButtonAction()
    {
        OnCenterButtonClicked -= CenterButtonEvent; // センターボタンのクリックイベントを解除
    }

    void CenterButtonEvent()
    {
        Transition(); // センターボタンがクリックされたときにTransitionを呼び出す
        PlayButtonSound(); // ボタンの効果音を再生
    }

    void PlayButtonSound()
    {
        if (_buttonSound == null)
        {
            _buttonSound = _centerButton.GetComponent<AudioSource>();
        }
        _buttonSound.Play();
    }
}
