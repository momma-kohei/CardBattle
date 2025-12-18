using UnityEngine;

public class CardPresenter
{
    CardListView _cardListView;
    CardListModel _cardListModel;

    int _maxHand = 7; // 手札の最大枚数
    bool _selectedSameElement = false; // 同じ属性が選択されたかどうかを判定するフラグ

    public CardPresenter(CardListView _cardListView, CardView _cardView) // コンストラクタ
    {
        this._cardListView = _cardListView; // ListViewのインスタンスを取得
        _cardListModel = new CardListModel(); // ListModelのインスタンスを生成
    }

    public void StartGame() // ゲーム開始時の初期化メソッド
    {
        _cardListModel.DrawCards(true, _maxHand); // プレイヤー１が最大枚数までカードを引く
        _cardListModel.DrawCards(false, _maxHand); // プレイヤー２が最大枚数までカードを引く
        _cardListView.ShowHand(_cardListModel); // 両者の手札を表示
    }
    public void NextTurn() // 次のターンへの移行処理メソッド
    {
        _cardListModel.ReloadHand(true); // プレイヤー１の手札をリロード
        _cardListModel.ReloadHand(false); // プレイヤー２の手札をリロード
        _cardListView.ShowHand(_cardListModel); // 両者の手札を表示
        _selectedSameElement = false; // 同じ属性が選択されたかどうかのフラグをリセット
    }
    // クリックイベント
    public void CompareElement(CardView _cardView) // クリックされたカードとareaList[0]の属性を比較するメソッド
    {
        if (_cardListModel.Area1.Count == 0 || _cardView.CardModel.element == _cardListModel.Area1[0].element) _selectedSameElement = true;
        else _selectedSameElement = false;
    }
    public void SelectHandCardByP1Click(CardView _cardView) // プレイヤー１の手札から選択してエリアにコピーし選択している手札を浮かせるメソッド
    {
        _cardListModel.SelectHandCard(_cardView.CardModel, _selectedSameElement, true);
        _cardListView.ShowArea1(_cardListModel);
        this.FloatSelectedP1Hand();
    }
    public void FloatSelectedP1Hand() // プレイヤー１の手札で選択されているカードを浮かせるメソッド
    {
        if (_cardListModel.Area1.Count > 0)
        {
            foreach (CardModel _handCardModel in _cardListModel.Hand1)
            {
                foreach (CardModel _areaCardModel in _cardListModel.Area1)
                {
                    if (_handCardModel == _areaCardModel)
                    {
                        _cardListView.MoveHand1(_handCardModel, true);
                        break;
                    }
                    else _cardListView.MoveHand1(_handCardModel, false);
                }
            }
        }
        else
        {
            foreach (CardModel _handCardModel in _cardListModel.Hand1)
            {
                _cardListView.MoveHand1(_handCardModel, false);
            }
            Debug.Log("Area1のリストにカードがありません。");
        }
    }
}
