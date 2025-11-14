using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckModel
{
    public List<int> deck; // デッキのカードIDリスト
    public List<int> handCardList; // 手札のカードIDリスト

    const int eleNum = 3; // 属性の数
    const int cardNumPerElement = 8; // 属性ごとのカードの数

    public CardDeckModel() // コンストラクタ
    {
        InitializeDeck(); // デッキを初期化
        ShuffleDeck(); // デッキをシャッフル
    }

    public void InitializeDeck() // デッキを初期化するメソッド
    {
        deck = new List<int>();

        for (int i = 1; i <= eleNum; i++)
        {
            for (int j = 1; j <= cardNumPerElement; j++)
            {
                int cardID = i * 10 + j; // 属性番号と序列番号でカードIDを生成
                deck.Add(cardID); // デッキにカードIDを追加
            }
        }
    }
    public void ShuffleDeck() // デッキをシャッフルするメソッド
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            int temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
    public void PickCardFromDeck(int pickNum) // デッキからカードを引くメソッド
    {
        handCardList = new List<int>();
        for (int i = 0; i < pickNum; i++)
        {
            if (deck.Count == 0) break; // デッキが空の場合は終了
            int cardID = deck[0]; // デッキの一番上のカードIDを取得
            deck.RemoveAt(0); // デッキからカードIDを削除
            handCardList.Add(cardID); // 手札にカードIDを追加
        }
        handCardList.Sort(); // 手札をカードID順にソート
        Debug.Log("Deck:" + deck.Count);
    }
}
