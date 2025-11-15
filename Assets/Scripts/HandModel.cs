using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HandModel
{
    List<int> _handCardList; // 手札のカードIDリスト
    List<int> _fieldCardList; // フィールドのカードIDリスト
    // 各リストのプロパティを設定
    public List<int> HandCardList { get { return _handCardList; } set { _handCardList = value; } }
    public List<int> FieldCardList { get { return _fieldCardList; } set { _fieldCardList = value; } }

    public void DrawCards(DeckModel02 deckModel, int num) // デッキからカードを引くメソッド
    {
        _handCardList = new List<int>();
        for (int i = 0; i < num; i++)
        {
            if (deckModel.Deck.Count == 0) break;// deckModel.ReloadDeck(); // デッキが空の場合は終了
            int cardID = deckModel.Deck[0]; // デッキの一番上のカードIDを取得
            deckModel.Deck.RemoveAt(0); // デッキからカードIDを削除
            _handCardList.Add(cardID); // 手札にカードIDを追加
        }
        _handCardList.Sort(); // 手札をカードID順にソート
    }
}
