//using UnityEditor.Search;
//using UnityEngine;

//public class CardListView : MonoBehaviour // リストモデルを元にカードビューを利用しつつ仕様の場所にカードを表示するクラス
//{
//    // 表示するカードプレハブを定義（表示場所によって異なる）
//    [SerializeField] public CardView _handCardPrefab1;   // プレハブを取得
//    [SerializeField] CardView _areaCardPrefab1;   // プレハブを取得
//    [SerializeField] CardView _handCardPrefab2;   // プレハブを取得
//    [SerializeField] CardView _areaCardPrefab2;   // プレハブを取得
//    public CardView HandCardPrefab1 { set { } get { return _handCardPrefab1; } } // プレイヤー１の手札プレハブのプロパティ
//    // 表示する場所のTransformを定義
//    [SerializeField] Transform _handTransform1; // プレイヤー１の手札のTransformを取得
//    [SerializeField] Transform _areaTransform1; // プレイヤー１のフィールドのTransfromを取得
//    [SerializeField] Transform _handTransform2; // プレイヤー２の手札のTransfromを取得
//    [SerializeField] Transform _areaTransform2; // プレイヤー２のフィールドのTransfromを取得

//    public void Start()
//    {
        
//    }

//    //public void ShowHand(PlayerModel _playerModel) // 両者の手札にカードを表示するメソッド
//    //{
//    //    if (_playerModel.IsMe)
//    //    {
//    //        // プレイヤー１の手札を表示
//    //        foreach (CardModel _cardModel in _playerModel.Hand)
//    //        {
//    //            CardView _card = Instantiate(_handCardPrefab1, _handTransform1);
//    //            _card.Show(_cardModel, this);
//    //        }
//    //    }
//    //    else
//    //    {
//    //        // プレイヤー２の手札を表示
//    //        foreach (CardModel _cardModel in _playerModel.Hand)
//    //        {
//    //            CardView _card = Instantiate(_handCardPrefab2, _handTransform2);
//    //            _card.Show(_cardModel, this);
//    //        }
//    //    }
//    //    // プレイヤー１の手札を表示
//    //    foreach (CardModel _cardModel in _cardListModel.Hand1)
//    //        {
//    //            CardView _card = Instantiate(_handCardPrefab1, _handTransform1);
//    //            _card.Show(_cardModel, this);
//    //            // 生成インスタンスごとに購読を登録（nullチェック）
//    //            if (_cardPresenter != null)
//    //            {
//    //                _card.OnCardClicked += _cardPresenter.CompareElement;
//    //                _card.OnCardClicked += _cardPresenter.SelectHandCardByP1Click;
//    //            }
//    //        }
//    //    // プレイヤー２の手札を表示
//    //    foreach (CardModel _cardModel in _cardListModel.Hand2)
//    //    {
//    //        CardView _card = Instantiate(_handCardPrefab2, _handTransform2);
//    //        _card.Show(_cardModel, this);
//    //    }
//    //}
//    public void ShowArea1(CardListModel _cardListModel) // プレイヤー１のエリアにカードを表示するメソッド
//    {
//        foreach (Transform n in _areaTransform1)
//        {
//            GameObject.Destroy(n.gameObject);
//        }
//        // プレイヤー１のエリアを表示
//        foreach (CardModel _cardModel in _cardListModel.Area1)
//        {
//            CardView _card = Instantiate(_areaCardPrefab1, _areaTransform1);
//            _card.Show(_cardModel, this);
//        }
//    }
//    public void ShowArea2(CardListModel _cardListModel) // プレイヤー２のエリアにカードを表示するメソッド
//    {
//        // プレイヤー１のエリアを表示
//        foreach (CardModel _cardModel in _cardListModel.Area2)
//        {
//            CardView _card = Instantiate(_areaCardPrefab2, _areaTransform2);
//            _card.Show(_cardModel, this);
//        }
//    }
//    public void MoveHand1(CardModel _cardModel, bool _toFloat) // プレイヤー１の手札の座標に干渉するメソッド
//    {
//        Transform _selectedCard = _handTransform1.Find("Card_" + _cardModel.cardID.ToString());
//        if (_selectedCard != null)
//        {
//            if (_toFloat)  _selectedCard.localPosition = new Vector3(_selectedCard.transform.localPosition.x, 10, _selectedCard.transform.localPosition.z);
//            else _selectedCard.localPosition = new Vector3(_selectedCard.transform.localPosition.x, 0, _selectedCard.transform.localPosition.z);
//        }  
//        else Debug.Log("選択されたカードが子オブジェクトに見つかりませんでした。");
//    }
//}
