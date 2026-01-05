using player;
using UnityEngine;

public class TablePresenter // ゲームを行う卓を表すクラス
{
    TableModel _model;
    Hand1View _hand1v;
    Hand2View _hand2v;
    Area1View _area1v;
    Area2View _area2v;
    PlayerView _player1v;
    PlayerView _player2v;

    public TablePresenter(Hand1View hand1v, Hand2View hand2v, Area1View area1v, Area2View area2v, PlayerView player1v, PlayerView player2v)
    {
        _model = new TableModel();

        _hand1v = hand1v;
        _hand2v = hand2v;
        _area1v = area1v;
        _area2v = area2v;
        _player1v = player1v;
        _player2v = player2v;

        // 初期表示
        _hand1v.Show(_model.GetPlayer1().GetHand());
        _hand2v.Show(_model.GetPlayer2().GetHand());
        _area1v.Show(_model.GetPlayer1().GetArea());
        _area2v.Show(_model.GetPlayer2().GetArea());
        // PlayerViewに関してはコンストラクタで表示済


    }

    /// <summary>
    /// Player１のカードがクリックされた時に呼ばれるメソッド
    /// </summary>
    /// <param name="card"></param>
    public void OnP1CardClicked(CardInfoView card)
    {
        _model.GetPlayer1().Toggle(card.GetModel()); // View -> Model
        _area1v.Show(_model.GetPlayer1().GetArea()); // Model -> View
        _player1v.ShowPower(_model.GetPlayer1()); // Model -> View

        foreach (CardInfoView civ in _hand1v.GetTransform().GetComponentsInChildren<CardInfoView>())
        {
            civ.OnCardClicked += OnP1CardClicked; // 場札に表示したカードにも同様のクリックイベントを登録
        }
    }

    /// <summary>
    /// TurnEndボタンがクリックされた時（１秒後）に呼ばれるメソッド
    /// </summary>
    public void OnTurnEndButtonPressed()
    {
        _model.PhaseBattle();
        // 戦闘後HPの表示
        _player1v.ShowHitPoint(_model.GetPlayer1());
        _player2v.ShowHitPoint(_model.GetPlayer2());
    }


}

    
