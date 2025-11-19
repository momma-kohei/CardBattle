using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckModel
{
    List<int> _deck; // デッキのカードIDリスト
    List<int> _trashCards; // 使用済みのカードIDリスト
    // 各リストのプロパティを設定
    public List<int> Deck { get { return _deck; } set { _deck = value; } }
    public List<int> TrashCards { get { return _trashCards; } set { _trashCards = value; } }
    const int eleNum = 3; // 属性の数
    const int cardNumPerElement = 9; // 属性ごとのカードの数

    public DeckModel() // コンストラクタ
    {
        InitializeDeck(); // デッキを初期化
        ShuffleDeck(); // デッキをシャッフル
    }

    public void InitializeDeck() // デッキを初期化するメソッド
    {
        _deck = new List<int>();

        for (int i = 1; i <= eleNum; i++)
        {
            for (int j = 1; j <= cardNumPerElement; j++)
            {
                int _cardID = i * 10 + j; // 属性番号と序列番号でカードIDを生成
                _deck.Add(_cardID); // デッキにカードIDを追加
            }
        }
    }
    public void ShuffleDeck() // デッキをシャッフルするメソッド
    {
        for (int i = 0; i < _deck.Count; i++)
        {
            int _randomIndex = Random.Range(0, _deck.Count);
            int _temp = _deck[i];
            _deck[i] = _deck[_randomIndex];
            _deck[_randomIndex] = _temp;
        }
    }
    public void ReloadDeck()
    {
        _deck = new List<int>(_trashCards); // 捨て札をデッキに戻す
        ShuffleDeck(); // デッキをシャッフル
    }
    public void TrashHandCards(HandModel _handModel)
    {
        foreach ( int _cardID in _handModel.FieldCardList)
        {
            _trashCards.Add(_cardID);
            _handModel.HandCardList.Remove(_cardID);
            _handModel.FieldCardList.Remove(_cardID);
        }
    }
}
