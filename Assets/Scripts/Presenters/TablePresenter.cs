using player;
using UnityEngine;

public class TablePresenter : MonoBehaviour // ゲームを行うテーブルを扱うクラス
{
    TableModel _model;
    // カード表示場所用View
    [SerializeField] Hand1View _hand1v;
    [SerializeField] Hand2View _hand2v;
    [SerializeField] Area1View _area1v;
    [SerializeField] Area2View _area2v;
    // プレイヤー情報表示用View
    [SerializeField] PlayerView _player1v;
    [SerializeField] PlayerView _player2v;

    public void Start() // 疑似コンストラクタ
    {
        _model = new TableModel(); // テーブルモデルを初期化

        // 先手後手の決定
        BattleSystem.CoinToss(_model);

        // 手札の補充
        _model.GetPlayer1().HandFill();
        _model.GetPlayer2().HandFill();

        // 初期状態で表示
        ShowAllCards();
        // PlayerViewに関してはコンストラクタで表示済のため不要
    }

    /// <summary>
    /// Player１のカードがクリックされた時に呼ばれるメソッド
    /// </summary>
    /// <param name="card"></param>
    void OnP1CardClicked(CardView card)
    {
        _model.GetPlayer1().Toggle(card.GetModel()); // View -> Model
        _area1v.Show(_model.GetPlayer1().GetArea()); // Model -> View
        _player1v.ShowPower(_model.GetPlayer1()); // Model -> View

        _hand1v.FloatSelectedHand(_model.GetPlayer1());

        SetClicEventArea1(); // 表示した場札にもイベントを登録
    }

    // ゲームの進行に関するメソッド
    #region Phase Method
    /// <summary>
    /// Attackフェーズを終了
    /// </summary>
    /// <param name="state">元のフェーズ</param>
    /// <returns>次のフェーズ</returns>
    /// <exception cref="System.Exception"></exception>
    public PhaseState EndPhaseAtk(PhaseState state)
    {
        ShowAllAreas(); // 場札を選択する処理はViewで完了している

        PlayerModel _attacker = _model.GetAttacker();
        if (!_model.CheckArea(_attacker))
        {
            BattleSystem.CPUAction(_attacker);
        }
        switch (state)
        {
            case PhaseState.Attack1:
                return PhaseState.Defence2;

            case PhaseState.Attack2:
                return PhaseState.Defence1;

            default: throw new System.Exception("Invarid State Input");
        }
    }

    /// <summary>
    /// Defenceフェーズを終了
    /// </summary>
    /// <returns>次のフェーズ</returns>
    public PhaseState EndPhaseDef()
    {
        ShowAllAreas(); // 場札を選択する処理はViewで完了している

        PlayerModel _defender = _model.GetDefender();
        if (!_model.CheckArea(_defender))
        {
            BattleSystem.CPUAction(_defender);
        }
        return PhaseState.Battle;
    }

    /// <summary>
    /// Battleフェーズを終了
    /// </summary>
    /// <returns>次のフェーズ</returns>
    public PhaseState EndPhaseBattle()
    {
        _model.Battle();

        ShowAllAreasOpen();
        ShowAllPowers();
        ShowAllHitPoints();

        return PhaseState.Idle;
    }

    /// <summary>
    /// 待機フェーズを終了
    /// </summary>
    /// <returns>次のフェーズ</returns>
    public PhaseState EndPhaseIdle()
    {
        if (_model.StartTurn())
        {
            ShowAllCards();
            ShowAllPowers();

            bool p1Atk = _model.GetPlayer1().GetPlayerStatus().GetIsAtk();
            if (p1Atk)
            {
                return PhaseState.Attack1;
            }
            else
            {
                return PhaseState.Attack2;
            }
        }
        else
        {
            return PhaseState.Gameover;
        }
    }
    #endregion

    // ゲームマネージャ―からアクセスするメソッド群
    #region For GameManager
    /// <summary>
    /// 自身のカードにイベントを登録するメソッド
    /// </summary>
    public void SetMyCardEvent()
    {
        SetClicEventHand1();
    }

    /// <summary>
    /// 自身のカードのイベントを解除するメソッド
    /// </summary>
    public void UnsetMyCardEvent()
    {
        foreach (CardView civ in _hand1v.GetTransform().GetComponentsInChildren<CardView>())
        {
            civ.OnCardClicked -= OnP1CardClicked;
        }
        foreach (CardView civ in _area1v.GetTransform().GetComponentsInChildren<CardView>())
        {
            civ.OnCardClicked -= OnP1CardClicked;
        }
    }

    /// <summary>
    /// テーブルモデルを取得
    /// </summary>
    /// <returns></returns>
    public TableModel GetTableModel()
    {
        return _model;
    }
    #endregion

    // ----------内部メソッド----------
    #region SubRoutine


    //カードにクリックイベントをセット
    void SetClicEventHand1()
    {
        foreach (CardView civ in _hand1v.GetTransform().GetComponentsInChildren<CardView>())
        {
            civ.OnCardClicked -= OnP1CardClicked; // ２重にならないよう一旦解除
            civ.OnCardClicked += OnP1CardClicked;
        }
    }
    void SetClicEventArea1()
    {
        foreach (CardView civ in _area1v.GetTransform().GetComponentsInChildren<CardView>())
        {
            civ.OnCardClicked -= OnP1CardClicked; // ２重にならないよう一旦解除
            civ.OnCardClicked += OnP1CardClicked;
        }
    }

    // 表示メソッド

    /// <summary>
    /// 全てのカードをデフォルト表示
    /// </summary>
    void ShowAllCards()
    {
        _hand1v.Show(_model.GetPlayer1().GetHand());
        _hand2v.Show(_model.GetPlayer2().GetHand());
        ShowAllAreas();
    }

    /// <summary>
    /// 全員の場札をデフォルト表示
    /// </summary>
    void ShowAllAreas()
    {
        _area1v.Show(_model.GetPlayer1().GetArea());
        _area2v.Show(_model.GetPlayer2().GetArea());
    }

    /// <summary>
    /// 全員の場札を開示
    /// </summary>
    void ShowAllAreasOpen()
    {
        _area1v.Show(_model.GetPlayer1().GetArea());
        _area2v.Open(_model.GetPlayer2().GetArea());
    }

    /// <summary>
    /// 全員のHPを表示
    /// </summary>
    void ShowAllHitPoints()
    {
        _player1v.ShowHitPoint(_model.GetPlayer1());
        _player2v.ShowHitPoint(_model.GetPlayer2());
    }

    /// <summary>
    /// 全員のパワーを表示
    /// </summary>
    void ShowAllPowers()
    {
        _player1v.ShowPower(_model.GetPlayer1());
        _player2v.ShowPower(_model.GetPlayer2());
    }
    #endregion
}


