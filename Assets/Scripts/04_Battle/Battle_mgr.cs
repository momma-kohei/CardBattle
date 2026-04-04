using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルに関する処理を行うクラス
/// </summary>
public class Battle_mgr : MonoBehaviour
{
    [SerializeField] Battle_ctl _battleC;
    [SerializeField] Battle_viw _battleV;
    [SerializeField] Field_mgr _fieldM;
    [SerializeField] Field_viw _fieldV;
    [SerializeField] CPU_mgr _cpuM;

    [SerializeField] Button _centerButton;
    [SerializeField] CenterButtonView _centerButtonV;

    [SerializeField] GameObject _resultPanel;
    [SerializeField] TextMeshProUGUI _resultText;

    [SerializeField] GameObject _hand1Panel;

    [SerializeField] TextMeshProUGUI _enemyName;

    int _maxHitPoint = 30;
    int _hitPoint1;
    int _hitPoint2;
    public int HitPoint1 { get => _hitPoint1; }
    public int HitPoint2 { get => _hitPoint2; }

    GamePhase _currentPhase;

    public Action OnCenterButtonClicked; // センターボタンがクリックされたときのイベント
    AudioSource _buttonSound;

    bool _isOnline = false; // オンラインかどうかのフラグ（必要に応じて設定）

    float _openDelay = 2f; // 場札を公開してから次のフェーズに遷移するまでの時間
    float _cpuDelay = 0.5f; // CPUの行動を呼び出すまでの遅延時間

    private void Awake()
    {
        if (_isOnline) // ----------------------------------------------------------------------------
        {
            // オンライン
            // (相手側)SetEnemyName(MyPlayer.Instance.PlayerName)
            // みたいな
        }
        else
        {
            // オフライン
            SetEnemyName("CPU"); // 敵の名前を表示
        }

        _hitPoint1 = _maxHitPoint;
        _hitPoint2 = _maxHitPoint;
    }

    void Start()
    {
        FillHands(); // 手札を満たす処理
        CoinToss(); // 先攻後攻の決定
    }

    void Update()
    {
        if (_currentPhase != GamePhase.Finish && Finished())
        {
            SetPhase(GamePhase.Finish);
        }
    }

    public void ResetField()
    {
        _fieldM.Trash(); // 場札をすべて捨てる
        _battleV.ShowPower1(_fieldM.GetArea1Power()); // パワー表示をリセット
        _battleV.ShowPower2(_fieldM.GetArea2Power());
        FillHands(); // 手札を満たす処理
    }

    void FillHands()
    {
        _fieldM.FillHand1(); // 自分の手札を満たす

        if (_isOnline) // ----------------------------------------------------------------------------
        {
            // オンライン
            // 相手プレイヤーの手札を満たす処理
            // (相手側)_fieldM.FillHand2();
        }
        else
        {
            // オフライン
            _cpuM.FillHand2(); // CPUの手札を満たす
        }
    }

    void CoinToss()
    {
        if (_isOnline) // ----------------------------------------------------------------------------
        {
            // オンライン
            // 先後の決定をうまいこと行う
        }
        else
        {
            // オフライン
            int coin = UnityEngine.Random.Range(0, 2); // コイントスで先攻後攻を決める
            if (coin == 0)
            {
                Debug.Log("Player1が先攻");
                SetPhase(GamePhase.PlayerSelect1);
            }
            else
            {
                Debug.Log("Player2が先攻");
                SetPhase(GamePhase.EnemySelect2);
            }
        }
    }

    public void Battle1() // player1が攻撃側の場合のバトル処理
    {
        _fieldM.OpenArea(); // 場札を公開
        _battleV.ShowPower2(_fieldM.GetArea2Power()); // 相手のパワーを公開
        _hitPoint2 -= _battleC.CalcDamage(true); // 相手のHPからダメージを引く
        _battleV.ShowHitPoint2(_hitPoint2); // HP表示
        if (!Finished()) Invoke(nameof(ResetField), _openDelay);
    }

    public void Battle2()
    {
        _fieldM.OpenArea(); // 場札を公開
        _battleV.ShowPower2(_fieldM.GetArea2Power()); // 相手のパワーを公開
        _hitPoint1 -= _battleC.CalcDamage(false); // 自分のHPからダメージを引く
        _battleV.ShowHitPoint1(_hitPoint1); // HP表示
        if (!Finished()) Invoke(nameof(ResetField), _openDelay);
    }

    public void SetPhase(GamePhase nextPhase)
    {
        _currentPhase = nextPhase;

        switch (nextPhase)
        {
            case GamePhase.PlayerSelect1:
                Debug.Log("PlayerSelect1フェーズに遷移");
                _battleV.ShowAttack1(); // 攻撃側の表示をPlayer1にする
                SetPlayerAction(); // 操作可能
                break;

            case GamePhase.EnemySelect1:
                Debug.Log("EnemySelect1フェーズに遷移");
                UnsetPlayerAction(); // 操作不可
                if (_isOnline) // ----------------------------------------------------------------------------
                {
                    // オンライン
                    // (相手側)SetPlayerAction();
                }
                else
                {
                    // オフライン
                    Invoke(nameof(CPU), _cpuDelay);
                }
                break;

            case GamePhase.Battle1:
                Debug.Log("Battle1フェーズに遷移 - Player1の攻撃処理を実行");
                UnsetPlayerAction(); // 操作不可

                // 数値処理やアニメーションなどのバトル処理を実行
                Battle1();

                Invoke(nameof(Transition), _openDelay); // 遅延して遷移
                break;

            case GamePhase.EnemySelect2:
                Debug.Log("EnemySelect2フェーズに遷移");
                _battleV.ShowAttack2(); // 攻撃側の表示をPlayer2にする
                UnsetPlayerAction(); // 操作不可
                if (_isOnline) // ----------------------------------------------------------------------------
                {
                    // オンライン
                    // (相手側)SetPlayerAction(); を呼び出すための通信
                }
                else
                {
                    // オフライン
                    Invoke(nameof(CPU), _cpuDelay);
                }
                break;

            case GamePhase.PlayerSelect2:
                Debug.Log("PlayerSelect2フェーズに遷移");
                SetPlayerAction(); // 操作可能
                break;

            case GamePhase.Battle2:
                Debug.Log("Battle2フェーズに遷移 - Player2の攻撃処理を実行");
                UnsetPlayerAction(); // 操作不可

                // 数値処理やアニメーションなどのバトル処理を実行
                Battle2();

                Invoke(nameof(Transition), _openDelay); // 遅延して遷移
                break;

            case GamePhase.Finish:
                Debug.Log("Finishフェーズに遷移 - ゲーム終了");
                UnsetPlayerAction(); // 操作不可

                // バトル終了処理
                _resultPanel.SetActive(true); // 結果パネルを表示
                if (_hitPoint1 <= 0)
                {
                    _resultText.text = "Lose...";
                    _resultText.color = Color.cyan; // 負けたときはテキストを青色にする
                }
                else if (_hitPoint2 <= 0)
                {
                    _resultText.text = "Win!!!";
                    _resultText.color = Color.red; // 勝ったときはテキストを赤色にする
                }
                break;
        }
    }

    public void Transition()
    {
        switch (_currentPhase)
        {
            case GamePhase.PlayerSelect1:
                if(!Finished()) SetPhase(GamePhase.EnemySelect1);
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
                if (!Finished()) SetPhase(GamePhase.EnemySelect2);
                break;
            case GamePhase.Battle2:
                SetPhase(GamePhase.PlayerSelect1);
                break;
        }
    }

    void CPU()
    {
        _cpuM.CallActionCPU(); // CPUの行動を呼び出し
        Transition(); // 遷移
    }

    bool Finished()
    {
        if (_hitPoint1 <= 0 || _hitPoint2 <= 0) return true;
        else return false;
    }

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

    public void SetPlayerAction()
    {
        _hand1Panel.SetActive(false); // 手札を表示
        foreach (Card_viw cardV in _fieldV.GetHand1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked += OnP1CardClicked; // Hand1のカードにクリックイベントを登録
        }
        foreach (Card_viw cardV in _fieldV.GetArea1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked += OnP1CardClicked; // Area1のカードにクリックイベントを登録
        }
    }

    public void UnsetPlayerAction()
    {
        _hand1Panel.SetActive(true); // 手札を非表示
        foreach (Card_viw cardV in _fieldV.GetHand1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked -= OnP1CardClicked; // Hand1のカードのクリックイベントを解除
        }
        foreach (Card_viw cardV in _fieldV.GetArea1Transform().GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked -= OnP1CardClicked; // Area1のカードのクリックイベントを解除
        }
        UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除
    }

    void OnP1CardClicked(Card_viw card)
    {
        // toggle & Area表示
        if (_fieldM.Toggle(card.GetModel()))
        {
            card.MoveCard(); // Hand移動
            card.PlaySound(); // 効果音再生

            UnsetCenterButtonAction(); // センターボタンのクリックイベントを解除
            if (!_fieldM.IsAreaEmpty())
            {
                SetCenterButtonAction(); // センターボタンのクリックイベントを登録
            }
        }
        // Power表示
        _battleV.ShowPower1(_fieldM.GetArea1Power()); // Area1のパワーを表示
    }

    
    // センターボタンに関する処理
    public void OnCenterButtonClick()
    {
        OnCenterButtonClicked?.Invoke(); // センターボタンがクリックされたときのイベントを呼び出す
    }

    void SetCenterButtonAction()
    {
        OnCenterButtonClicked += CenterButtonEvent; // センターボタンにクリックイベントを登録
        _centerButtonV.SetButtonText(true); // センターボタンを押せる状態の見た目にする
    }

    void UnsetCenterButtonAction()
    {
        OnCenterButtonClicked -= CenterButtonEvent; // センターボタンのクリックイベントを解除
        _centerButtonV.SetButtonText(false); // センターボタンを押せない状態の見た目にする
    }

    void CenterButtonEvent()
    {
        Transition(); // センターボタンがクリックされたときにTransitionを呼び出す
        if (_isOnline) // ----------------------------------------------------------------------------
        {
            // オンライン
            // (相手側)Transition(); を呼び出すための通信
        }
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

    public void SetEnemyName(string name)
    {
        _enemyName.text = "vs " + name; // 敵の名前を表示
    }
}
