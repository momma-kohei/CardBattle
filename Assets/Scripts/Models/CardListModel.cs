using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// カードを管理するリストモデルクラス
/// カードリストに関する操作はこのクラスで完結するべき
/// </summary>
public class CardListModel
{
    // 必要なリストを定義
    List<CardModel> _deck;      // デッキのカードリスト
    List<CardModel> _hand1;     // プレイヤー１の手札のカードリスト
    List<CardModel> _hand2;     // プレイヤー２の手札のカードリスト
    List<CardModel> _area1;     // プレイヤー１のエリアのカードリスト
    List<CardModel> _area2;     // プレイヤー２のエリアのカードリスト
    List<CardModel> _trash;     // 捨て札のカードリスト
    // 各リストのプロパティを設定
    public List<CardModel> Deck { get { return _deck; } }
    public List<CardModel> Hand1 { get { return _hand1; } }
    public List<CardModel> Hand2 { get { return _hand2; } }
    public List<CardModel> Area1 { get { return _area1; } }
    public List<CardModel> Area2 { get { return _area2; } }
    public List<CardModel> Trash { get { return _trash; } }

    // private bool _isFirstClick = true; // 最初のクリックかどうかを判定するフラグ

    public CardListModel() // コンストラクタ
    {
        _deck = new List<CardModel>();
        InitializeDeck(); // デッキを初期化
        _hand1 = new List<CardModel>();
        _hand2 = new List<CardModel>();
        _area1 = new List<CardModel>();
        _area2 = new List<CardModel>();
        _trash = new List<CardModel>();
    }

    // デッキの初期化に関するメソッドを実装
    void InitializeDeck() // デッキを初期化するメソッド（属性1-3、各9枚ずつ、カードID:11-19,21-29,31-39）
    {
        for (int i = 1; i <= 3; i++) for (int j = 1; j <= 9; j++) _deck.Add(new CardModel(i * 10 + j));
        ShuffleDeck(); // デッキをシャッフル
    }
    void ReloadDeck() // 捨て札をデッキに戻してシャッフルするメソッド
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

    // リストを操作するメソッドを実装
    /// <summary>
    /// デッキから枚数を指定してカードを引くメソッド
    /// </summary>
    /// <param name="_isP1">プレイヤー１の操作かどうか</param>
    /// <param name="_num">カードを引く枚数</param>
    public void DrawCards(bool _isP1, int _num) 
    {
        List<CardModel> _hand = _isP1 ? _hand1 : _hand2; // ブールによって手札リストを切り替え
        for (int i = 0; i < _num; i++) // 指定枚数分ループ
        {
            if (_deck.Count <= 0) ReloadDeck(); // デッキが足りない場合リロード
            if (_deck.Count > 0) // デッキにカードが残っていることを確認
            {
                _hand.Add(_deck[0]); // 手札にデッキの一番上のカードを追加
                _deck.RemoveAt(0); // デッキの一番上のカードを削除
                _hand.Sort((a, b) => a.cardID.CompareTo(b.cardID)); // 手札をカードID昇順にソート
            } else Debug.Log("デッキと捨て札が空です。カードを引けません。");
        }
    }
    /// <summary>
    /// 手札を選択して場札に出す/戻すメソッド
    /// </summary>
    /// <param name="_isFirst">１回目の選択かどうか</param>
    /// <param name="_isP1">プレイヤー１の操作かどうか</param>
    /// <param name="_handIndex">選択する手札のインデックス</param>
    public void SelectHandCard(CardModel _cardModel, bool _selectSameElement, bool _isP1)
    {
        List<CardModel> _hand = _isP1 ? _hand1 : _hand2; // ブールによって手札リストを切り替え
        List<CardModel> _area = _isP1 ? _area1 : _area2; // ブールによって場札リストを切り替え
        int _handIndex = _hand.IndexOf(_cardModel); // 手札リストから選択されたカードのインデックスを取得
        Debug.Log($"選択された手札のインデックス: {_handIndex}");
        if (_handIndex >= 0 && _handIndex < _hand.Count && (_area1.Count == 0 ||  _cardModel.element == _area1[0].element )) // 指定されたインデックスが正当であることと属性一致を確認
        {
            if (_selectSameElement)
            {
                if (_cardModel.isSelected)
                {
                    Debug.Log("選択済みなので場札から削除します。");
                    _area.Remove(_hand[_handIndex]); // 選択済みならば場札から削除
                    _cardModel.isSelected = false;
                }
                else
                {
                    Debug.Log("未選択なので場札に追加します。");
                    _area.Add(_hand[_handIndex]); // 未選択ならば場札に追加
                    _cardModel.isSelected = true;
                }
            }
        }
        else Debug.Log("手札のインデックスが不正です。");
    }
    public void ReloadHand(bool _isP1)
    {
        List<CardModel> _hand = _isP1 ? _hand1 : _hand2; // ブールによって手札リストを切り替え
        List<CardModel> _area = _isP1 ? _area1 : _area2; // ブールによって場札リストを切り替え
        int _reloadNum = _area.Count; // 場札の枚数を取得
        TrashCardFromArea(_isP1);
        DrawCards(_isP1, _reloadNum); // 捨てた場札の枚数分手札を引く
    }
    private void TrashCardFromArea(bool _isP1) // 選択して場札に出したカードを捨て札に移動するメソッド
    {
        List<CardModel> _hand = _isP1 ? _hand1 : _hand2; // ブールによって手札リストを切り替え
        List<CardModel> _area = _isP1 ? _area1 : _area2; // ブールによって場札リストを切り替え
        for (int i = 0; i < _area.Count; i++)
        {
            CardModel _trashedCard = _area[i];
            _area.RemoveAt(i);
            _hand.Remove(_trashedCard);
            _trash.Add(_trashedCard);
        }
    }

    // 対戦用メソッド

    /// <summary>
    /// 場札内のカードのパワーの合計を返すメソッド
    /// </summary>
    /// <param name="_isP1">プレイヤー１の操作かどうか</param>
    /// <returns>場札内のカードのパワーの合計</returns>
    public int GetAreaPower(bool _isP1)
    {
        int _sum = 0; // パワーの合計値
        List<CardModel> _area = _isP1 ? _area1 : _area2; // ブールによって場札リストを切り替え
        foreach (CardModel card in _area) _sum += card.power; // 場札内のカードのパワーを合計
        return _sum;
    }
    /// <summary>
    /// 場札のカードの属性を返すメソッド
    /// </summary>
    /// <param name="_isP1">プレイヤー１の操作かどうか</param>
    /// <returns>場札の先頭のカードの属性</returns>
    public ElementType GetAreaElement(bool _isP1)
    {
        List<CardModel> _area = _isP1 ? _area1 : _area2; // ブールによって場札リストを切り替え
        return _area[0].element;
    }
}
