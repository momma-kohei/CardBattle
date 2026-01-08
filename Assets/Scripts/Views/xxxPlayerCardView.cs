//using NUnit.Framework;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCardView : MonoBehaviour
//{
//    [SerializeField] CardView _cardPrefab; // カードプレハブの参照
//    [SerializeField] Transform _handTransform; // 手札を表示するTransformの参照
//    [SerializeField] Transform _areaTransform; // 場札を表示するTransformの参照

//    public Transform HandTransform { get { return _handTransform; } } // 手札表示Transformのプロパティ
//    public Transform AreaTransform { get { return _areaTransform; } } // 場札表示Transformのプロパティ

//    /// <summary>
//    /// PlayerModelの(Hand/Area)を状態を指定して表示するメソッド
//    /// </summary>
//    /// <param name="_playerModel">PlayerModel</param>
//    /// <param name="_cardPlace">"Hand" or "Area"</param>
//    /// <param name="_cardState">'F', 'B' or 'E'</param>
//    public void ShowCards(PlayerModel _playerModel, string _cardPlace, char _cardState)
//    {
//        Transform _transform;
//        List<CardModel> _cards;
//        switch (_cardPlace)
//        {
//            case "Hand":
//                _transform = _handTransform;
//                _cards = _playerModel.Hand;
//                break;
//            case "Area":
//                _transform = _areaTransform;
//                _cards = _playerModel.Area;
//                break;
//            default:
//                Debug.LogError("Invalid card place. Use 'Hand' or 'Area'");
//                return;
//        }
//        foreach (Transform _oldcard in _transform) GameObject.Destroy(_oldcard.gameObject);
//        foreach (CardModel _cardModel in _cards)
//        {
//            switch (_cardState)
//            {
//                case 'F':
//                    {
//                        CardView _card = Instantiate(_cardPrefab, _transform);
//                        _card.ShowCard(_cardModel, 'F'); // 表面を表示
//                        break;
//                    }
//                case 'B':
//                    {
//                        CardView _card = Instantiate(_cardPrefab, _transform);
//                        _card.ShowCard(_cardModel, 'B'); // 裏面を表示
//                        break;
//                    }
//                case 'E':
//                    {
//                        CardView _card = Instantiate(_cardPrefab, _transform);
//                        _card.ShowCard(_cardModel, 'E'); // 属性面を表示
//                        break;
//                    }
//                default:
//                    {
//                        Debug.LogError("Invalid card state. Use 'F', 'B', or 'E'.");
//                        break;
//                    }
//            }
//        }
//    }



//    // 選択されたカードを浮かせる処理を実装
//    // ただ必須ではないため後回し
//    //public void FloatHand(PlayerModel _playerModel) // プレイヤー１の手札で選択されているカードを浮かせるメソッド
//    //{
//    //    if (_playerModel.Area.Count > 0)
//    //    {
//    //        foreach (CardModel _handCardModel in _playerModel.Hand)
//    //        {
//    //            foreach (CardModel _areaCardModel in _playerModel.Area)
//    //            {
//    //                if (_handCardModel == _areaCardModel)
//    //                {
//    //                    _cardListView.MoveHand1(_handCardModel, true);
//    //                    break;
//    //                }
//    //                else _cardListView.MoveHand1(_handCardModel, false);
//    //            }
//    //        }
//    //    }
//    //    else
//    //    {
//    //        foreach (CardModel _handCardModel in _cardListModel.Hand1)
//    //        {
//    //            _cardListView.MoveHand1(_handCardModel, false);
//    //        }
//    //        Debug.Log("Area1のリストにカードがありません。");
//    //    }
//    //}
//}
