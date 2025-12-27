using UnityEngine;

public class CardPresenter
{
    PlayerModel _playerModel1; // プレイヤーモデルのインスタンスを保持する変数
    PlayerModel _playerModel2; // プレイヤーモデルのインスタンスを保持する変数
    PlayerCardView _playerCardView1; // PlayerCardViewのインスタンスを保持する変数
    PlayerCardView _playerCardView2; // PlayerCardViewのインスタンスを保持する変数

    // bool _selectedSameElement = false; // 同じ属性が選択されたかどうかを判定するフラグ

    public CardPresenter(PlayerModel playerModel1, PlayerModel playerModel2, PlayerCardView playerCardView1, PlayerCardView playerCardView2) // コンストラクタ
    {
        _playerModel1 = playerModel1;
        _playerModel2 = playerModel2;
        _playerCardView1 = playerCardView1;
        _playerCardView2 = playerCardView2;
    }

    public void StartTurn() // ターン開始時の処理メソッド
    {
        StartTurnPlayer(_playerCardView1, _playerModel1, true); // プレイヤー１のターン開始処理
        StartTurnPlayer(_playerCardView2, _playerModel2, false); // プレイヤー２のターン開始処理

        //if (_playerModel1.IsMe)
        //{
        //    StartTurnPlayer(_playerCardView, _playerModel1); // プレイヤー１のターン開始処理
        //    StartTurnPlayer(_opponentCardView, _playerModel2); // プレイヤー２のターン開始処理
        //}
        //else
        //{
        //    StartTurnPlayer(_opponentCardView, _playerModel1); // プレイヤー１のターン開始処理
        //    StartTurnPlayer(_playerCardView, _playerModel2); // プレイヤー２のターン開始処理
        //}
    }

    public void OnCardClicked(CardView _cardView)
    {
        _playerModel1.SelectCard(_cardView.CardModel); // プレイヤー１の手札からエリアにカードを選択してコピー
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // プレイヤー１の場札を表示
        //_playerCardView1.ShowAreaCards(_playerModel1); // プレイヤー１の場札を表示
        foreach (CardView _cv in _playerCardView1.AreaTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked += OnCardClicked; // クリックイベントを登録

        //if (_playerModel1.IsMe) {
        //    _playerModel1.SelectCard(_cardView.CardModel); // プレイヤー１の手札からエリアにカードを選択してコピー
        //    _playerCardView.ShowAreaCards(_playerModel1); // プレイヤー１の場札を表示
        //    foreach (CardView _cv in _playerCardView.AreaTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked += OnCardClicked; // クリックイベントを登録
        //}
        //else
        //{
        //    _playerModel2.SelectCard(_cardView.CardModel); // プレイヤー２の手札からエリアにカードを選択してコピー
        //    _playerCardView.ShowAreaCards(_playerModel2); // プレイヤー２の場札を表示
        //}

        // 手札を浮かせる処理を追加する場合はここに記述
    }

    public void EndAttackerPhase() // アタッカーフェーズの処理メソッド
    {
        // 自分がアタッカーの場合は既に場札が表示されている
        // 手札のクリックイベントを解除
        if (_playerModel1.IsAttacker) foreach (CardView _cv in _playerCardView1.HandTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked -= OnCardClicked; // クリックイベントを削除

        // Areaを同期

        // 相手がアタッカーの場合，相手の場札を表示
        if (_playerModel2.IsAttacker) _playerCardView2.ShowCards(_playerModel2, "Area", 'E'); // プレイヤー２の場札を表示
        //if (_playerModel2.IsAttacker) _playerCardView2.ShowAreaElements(_playerModel2); // プレイヤー２の場札を表示

        //if (_playerModel1.IsMe && _playerModel1.IsAttacker) _opponentCardView.ShowAreaElements(_playerModel2); // プレイヤー２の場札を表示
        //else if (_playerModel2.IsMe && _playerModel2.IsAttacker) _opponentCardView.ShowAreaElements(_playerModel1); // プレイヤー１の場札を表示
    }

    public void EndDefenderPhase() // ディフェンダーフェーズの処理メソッド
    {
        // 自分がディフェンダーの場合は既に場札が表示されている
        // 手札のクリックイベントを解除
        if (!_playerModel1.IsAttacker) foreach (CardView _cv in _playerCardView1.HandTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked -= OnCardClicked; // クリックイベントを解除

        // Areaを同期

        // 相手がアタッカーの場合，相手の場札を表示
        if (!_playerModel2.IsAttacker) _playerCardView2.ShowCards(_playerModel2, "Area", 'E'); // プレイヤー２の場札を表示
        //if (!_playerModel2.IsAttacker) _playerCardView2.ShowAreaCards(_playerModel2); // プレイヤー２の場札を表示

        //if (_playerCardView1.IsMe) _playerCardView1.ShowAreaCards(_playerModel2); // プレイヤー２の場札を開示
        //else _playerCardView2.ShowAreaCards(_playerModel1); // プレイヤー１の場札を開示
    }

    public void BattlePhase() // バトルフェーズの処理メソッド
    {
        _playerModel1.CalculateDamege(_playerModel2); // プレイヤー１がプレイヤー２にダメージを与える
        _playerModel2.CalculateDamege(_playerModel1); // プレイヤー２がプレイヤー１にダメージを与える

        // HP表示の更新
    }

    // サブルーチン
    void StartTurnPlayer(PlayerCardView _playerCardView, PlayerModel _playerModel, bool _isMyPlayer)
    {
        char _areaState;
        char _handState;

        if (_isMyPlayer) {
            _areaState = 'F';
            _handState = 'F';
        }
        else {
            _areaState = 'B';
            _handState = 'E';
        }

        _playerCardView.ShowCards(_playerModel, "Area", _areaState); // プレイヤーの場札を更新
        //_playerCardView.ShowAreaCards(_playerModel); // プレイヤーの場札を削除
        _playerModel.DrawCards(); // プレイヤーの手札を補充
        _playerCardView.ShowCards(_playerModel, "Hand", _handState); // プレイヤーの手札を表示
        //_playerCardView.ShowHandCards(_playerModel, _cardState); // プレイヤーの手札を表示
        if (_playerModel == _playerModel1) foreach (CardView _cardView in _playerCardView.HandTransform.GetComponentsInChildren<CardView>()) _cardView.OnClicked += OnCardClicked; // クリックイベントを登録
    }


    //public void NextTurn()
    //{
    //    _cardListModel.ReloadHand(true); // プレイヤー１の手札をリロード
    //    _cardListModel.ReloadHand(false); // プレイヤー２の手札をリロード
    //    _cardListView.ShowHand(_cardListModel); // 両者の手札を表示
    //    _selectedSameElement = false; // 同じ属性が選択されたかどうかのフラグをリセット
    //}
    //// クリックイベント
    //public void CompareElement(CardView _cardView) // クリックされたカードとareaList[0]の属性を比較するメソッド
    //{
    //    if (_cardListModel.Area1.Count == 0 || _cardView.CardModel.element == _cardListModel.Area1[0].element) _selectedSameElement = true;
    //    else _selectedSameElement = false;
    //}
    //public void SelectHandCardByP1Click(CardView _cardView) // プレイヤー１の手札から選択してエリアにコピーし選択している手札を浮かせるメソッド
    //{
    //    _cardListModel.SelectHandCard(_cardView.CardModel, _selectedSameElement, true);
    //    _cardListView.ShowArea1(_cardListModel);
    //    this.FloatSelectedP1Hand();
    //}
    //public void FloatSelectedP1Hand() // プレイヤー１の手札で選択されているカードを浮かせるメソッド
    //{
    //    if (_cardListModel.Area1.Count > 0)
    //    {
    //        foreach (CardModel _handCardModel in _cardListModel.Hand1)
    //        {
    //            foreach (CardModel _areaCardModel in _cardListModel.Area1)
    //            {
    //                if (_handCardModel == _areaCardModel)
    //                {
    //                    _cardListView.MoveHand1(_handCardModel, true);
    //                    break;
    //                }
    //                else _cardListView.MoveHand1(_handCardModel, false);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (CardModel _handCardModel in _cardListModel.Hand1)
    //        {
    //            _cardListView.MoveHand1(_handCardModel, false);
    //        }
    //        Debug.Log("Area1のリストにカードがありません。");
    //    }
    //}
}
