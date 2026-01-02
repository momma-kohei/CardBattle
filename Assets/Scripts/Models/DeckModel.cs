using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckModel // 共通のデッキのモデル
{
    List<CardModel> _deck;      // デッキのカードリスト
    List<CardModel> _trash;     // 捨て札のカードリスト
    public List<CardModel> Deck { get { return _deck; } } // デッキのプロパティ
    public List<CardModel> Trash { get { return _trash; } } // 捨て札のプロパティ

    public DeckModel() // コンストラクタ
    {
        _deck = new List<CardModel>();
        _trash = new List<CardModel>();

        InitializeDeck(); // デッキを初期化
    }

    // デッキ内部に関するメソッド
    void InitializeDeck() // デッキを初期化するメソッド（属性1-3、各9枚ずつ、カードID:11-19,21-29,31-39）
    {
        for (int i = 1; i <= 3; i++) for (int j = 1; j <= 9; j++) _deck.Add(new CardModel(i * 10 + j));
        ShuffleDeck(); // デッキをシャッフル
    }

    public void ReloadDeck() // 捨て札をデッキに戻してシャッフルするメソッド
    {
        _deck = new List<CardModel>(_trash); // 捨て札をデッキに戻す
        _trash.Clear(); // 捨て札リストをクリア
        ShuffleDeck(); // デッキをシャッフル
    }

    void ShuffleDeck() // デッキをシャッフルするメソッド
    {
        for (int i = 0; i < _deck.Count; i++)
        {
            int _randomIndex = UnityEngine.Random.Range(0, _deck.Count);
            CardModel _temp = _deck[i];
            _deck[i] = _deck[_randomIndex];
            _deck[_randomIndex] = _temp;
        }
    }

    // デッキ操作に関するメソッド
    public CardModel PushCard() // デッキからカードを1枚引くメソッド
    {
        if (_deck.Count == 0) ReloadDeck(); // デッキが空の場合、捨て札をデッキに戻してシャッフル
        CardModel drawnCard = _deck[0]; // デッキの一番上のカードを取得
        _deck.RemoveAt(0); // 取得したカードをデッキから削除
        return drawnCard; // 引いたカードを返す
    }

    public void PullTrashCard(CardModel card) // カードを捨て札に追加するメソッド
    {
        _trash.Add(card); // 捨て札リストにカードを追加
    }

    // その他のデッキ関連のメソッドをここに追加可能
    public void PrintDeckNum() { Debug.Log("デッキ枚数: " + _deck.Count + " 捨て札枚数: " + _trash.Count); }
}
