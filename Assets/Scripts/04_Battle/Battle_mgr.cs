using UnityEngine;

/// <summary>
/// バトルに関する処理を行うクラス
/// </summary>
public class Battle_mgr : MonoBehaviour
{
    [SerializeField] Battle_ctl _battleC;
    [SerializeField] Field_mgr _fieldM;
    [SerializeField] Field_viw _fieldV;
    [SerializeField] CPU_mgr _cpuM;

    int _hitPoint1;
    int _hitPoint2;
    bool _isPlayerSelect;

    public int HitPoint1 { get => _hitPoint1; set => _hitPoint1 = value; }
    public int HitPoint2 { get => _hitPoint2; set => _hitPoint2 = value; }

    GamePhase _currentPhase;


    void Start()
    {
        SetPhase(GamePhase.PlayerSelect1);

        // オンラインの場合は、立場の弱い方（クライアント）がここでSetPhase(GamePhase.EnemySelect1)とか
    }

    void Update()
    {
        if (_isPlayerSelect)
        {
            // カードクリックイベントを登録
        }
        else
        {
            // カードクリックイベントを解除
        }
    }

    public void Battle1() // player1が攻撃側の場合のバトル処理
    {
        _hitPoint2 -= _battleC.CalcDamage(true); // 相手のHPからダメージを引く
    }

    public void Battle2()
    {
        _hitPoint1 -= _battleC.CalcDamage(false); // 自分ののHPからダメージを引く
    }

    public void SetPhase(GamePhase nextPhase)
    {
        _currentPhase = nextPhase;

        switch (nextPhase)
        {
            case GamePhase.PlayerSelect1:

                // 自分のクリックを有効化
                _isPlayerSelect = true;
                break;
            
            case GamePhase.EnemySelect1:
                _isPlayerSelect = false;
                // 相手のクリックを有効化
                // 自分は待機
                break;
            case GamePhase.EnemySelect2:
                _isPlayerSelect = false;
                // 相手のクリックを有効化
                // 自分は待機
                break;
            case GamePhase.PlayerSelect2:
                // 自分のクリックを有効化
                _isPlayerSelect = true;
                break;
            case GamePhase.Battle1:
                _isPlayerSelect = false;
                // 数値処理やアニメーションなどのバトル処理を実行
                Battle1();
                break;
            case GamePhase.Battle2:
                _isPlayerSelect = false;
                // 数値処理やアニメーションなどのバトル処理を実行
                Battle2();
                break;
            case GamePhase.Finish:
                _isPlayerSelect = false;
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
                Finish(); // 条件を満たしていればFinishに遷移
                SetPhase(GamePhase.EnemySelect2);
                break;
            case GamePhase.Battle2:
                Finish(); // 条件を満たしていればFinishに遷移
                SetPhase(GamePhase.PlayerSelect1);
                break;
        }
        // 自分のセレクト終了時に自分と相手のTransitionを呼び出す
    }

    public void Finish()
    {
        if (_hitPoint1 <= 0 || _hitPoint2 <= 0) SetPhase(GamePhase.Finish);
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

    void SetP1HandAction()
    {
        //foreach (Card_viw cardV in _fieldV.GetHand1())
        //{
        //    cardV.SetClickAction(OnP1CardClicked);
        //}
    }

    void OnP1CardClicked()
    {
        // toggle
        // Hand移動
        // Area表示
        // Power表示

        // 表示したAreaにもイベントを登録
    }
}
