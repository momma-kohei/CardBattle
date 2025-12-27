using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardView : MonoBehaviour
{
    [SerializeField] CardView _cardPrefab; // カードプレハブの参照
    //[SerializeField] CardView _handCardPrefab; // カードプレハブの参照
    //[SerializeField] CardView _areaCardPrefab; // カードプレハブの参照
    [SerializeField] Transform _handTransform; // 手札を表示するTransformの参照
    [SerializeField] Transform _areaTransform; // 場札を表示するTransformの参照

    public Transform HandTransform { get { return _handTransform; } } // 手札表示Transformのプロパティ
    public Transform AreaTransform { get { return _areaTransform; } } // 場札表示Transformのプロパティ

    public void ShowCards(PlayerModel _playerModel, string _cardPlace, char _cardState)
    {
        Transform _transform;
        List<CardModel> _cards;
        switch (_cardPlace)
        {
            case "Hand":
                _transform = _handTransform;
                _cards = _playerModel.Hand;
                break;
            case "Area":
                _transform = _areaTransform;
                _cards = _playerModel.Area;
                break;
            default:
                Debug.LogError("Invalid card place. Use 'Hand' or 'Area'");
                return;
        }
        foreach (Transform _oldcard in _transform) GameObject.Destroy(_oldcard.gameObject);
        foreach (CardModel _cardModel in _cards)
        {
            switch (_cardState)
            {
                case 'F':
                    {
                        CardView _card = Instantiate(_cardPrefab, _transform);
                        _card.ShowCard(_cardModel, 'F'); // 表面を表示
                        break;
                    }
                case 'B':
                    {
                        CardView _card = Instantiate(_cardPrefab, _transform);
                        _card.ShowCard(_cardModel, 'B'); // 裏面を表示
                        break;
                    }
                case 'E':
                    {
                        CardView _card = Instantiate(_cardPrefab, _transform);
                        _card.ShowCard(_cardModel, 'E'); // 属性面を表示
                        break;
                    }
                default:
                    {
                        Debug.LogError("Invalid card state. Use 'F', 'B', or 'E'.");
                        break;
                    }
            }
        }
    }

    //public void ShowHandCards(PlayerModel _playerModel, char _cardState)
    //{
    //    // 手札を表示
    //    foreach (Transform _oldcard in _handTransform) GameObject.Destroy(_oldcard.gameObject);
    //    foreach (CardModel _cardModel in _playerModel.Hand)
    //    {
    //        switch (_cardState)
    //        {
    //            case 'F':
    //                {
    //                    CardView _card = Instantiate(_handCardPrefab, _handTransform, true);
    //                    _card.ShowCard(_cardModel, 'F'); // 表面を表示
    //                    break;
    //                }
    //            case 'B':
    //                {
    //                    CardView _card = Instantiate(_handCardPrefab, _handTransform, false);
    //                    _card.ShowCard(_cardModel, 'B'); // 裏面を表示
    //                    break;
    //                }
    //            case 'E':
    //                {
    //                    CardView _card = Instantiate(_handCardPrefab, _handTransform, false);
    //                    _card.ShowCard(_cardModel, 'E'); // 属性面を表示
    //                    break;
    //                }
    //            default:
    //                {
    //                    Debug.LogError("Invalid card state. Use 'F', 'B', or 'E'.");
    //                    break;
    //                }
    //        }

            
    //        //CardView _card = Instantiate(_handCardPrefab, _handTransform, false);
    //        //if (_playerModel.IsMe) _card.ShowCard(_cardModel, 'F'); // 自分の手札は表面を表示
    //        //else _card.ShowCard(_cardModel, 'B'); // 相手の手札は裏面を表示
    //    }
    //}

    //public void ShowAreaCards(PlayerModel _playerModel, char _cardState)
    //{
    //    // 場札を表示
    //    foreach (Transform _oldcard in _areaTransform) GameObject.Destroy(_oldcard.gameObject);
    //    switch (_cardState)
    //    {
    //        case 'F':
    //            {
    //                foreach (CardModel _cardModel in _playerModel.Area)
    //                {
    //                    CardView _card = Instantiate(_areaCardPrefab, _areaTransform, true);
    //                    _card.ShowCard(_cardModel, 'F'); // 表面を表示
    //                }
    //                break;
    //            }
    //        case 'E':
    //            {
    //                foreach (CardModel _cardModel in _playerModel.Area)
    //                {
    //                    CardView _card = Instantiate(_areaCardPrefab, _areaTransform, false);
    //                    _card.ShowCard(_cardModel, 'E'); // 属性面を表示
    //                }
    //                break;
    //            }
    //        default:
    //            {
    //                Debug.LogError("Invalid card state. Use 'F' or 'E'.");
    //                break;
    //            }
    //    }

    //    foreach (CardModel _cardModel in _playerModel.Area)
    //    {
    //        CardView _card = Instantiate(_areaCardPrefab, _areaTransform, true);
    //        if (_playerModel.IsMe) _card.ShowCard(_cardModel, 'F'); // 自分の場札は表面を表示
    //        else _card.ShowCard(_cardModel, 'F'); // 相手の場札は表面を表示
    //    }
    //}



    //public void ShowAreaElements(PlayerModel _playerModel)
    //{
    //    // 場札を表示
    //    foreach (Transform _oldcard in _areaTransform) GameObject.Destroy(_oldcard.gameObject);
    //    foreach (CardModel _cardModel in _playerModel.Area)
    //    {
    //        CardView _card = Instantiate(_areaCardPrefab, _areaTransform, false);
    //        if (_playerModel.IsMe) _card.ShowCard(_cardModel, 'F'); // 自分の場札は表面を表示
    //        else _card.ShowCard(_cardModel, 'E'); // 相手の場札は属性面を表示
    //    }
    //}


    // 選択されたカードを浮かせる処理を実装
    // ただ必須ではないため後回し
    //public void FloatHand(PlayerModel _playerModel) // プレイヤー１の手札で選択されているカードを浮かせるメソッド
    //{
    //    if (_playerModel.Area.Count > 0)
    //    {
    //        foreach (CardModel _handCardModel in _playerModel.Hand)
    //        {
    //            foreach (CardModel _areaCardModel in _playerModel.Area)
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
