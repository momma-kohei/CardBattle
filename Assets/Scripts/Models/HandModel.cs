using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandModel
{
    List<int> _handCardList; // 手札のカードIDリスト
    List<int> _fieldCardList; // フィールドのカードIDリスト
    // 各リストのプロパティを設定
    public List<int> HandCardList { get { return _handCardList; } set { _handCardList = value; } }
    public List<int> FieldCardList { get { return _fieldCardList; } set { _fieldCardList = value; } }

    public void Update()
    {
        if (_fieldCardList != null && _fieldCardList.Count > 0)
        {
            for (int i = 0; i < _fieldCardList.Count; i++)
            {
                Debug.Log("FieldCardID:" + _fieldCardList[i]);
            }
        }
    }

    public HandModel()
    {
        _handCardList = new List<int>();
        _fieldCardList = new List<int>();
    }

    public void DrawCards(DeckModel _deckModel, int _num) // デッキからカードを引くメソッド
    {
        _handCardList = new List<int>();
        for (int i = 0; i < _num; i++)
        {
            if (_deckModel.Deck.Count == 0) break;// deckModel.ReloadDeck(); // デッキが空の場合は終了
            int _cardID = _deckModel.Deck[0]; // デッキの一番上のカードIDを取得
            _deckModel.Deck.RemoveAt(0); // デッキからカードIDを削除
            _handCardList.Add(_cardID); // 手札にカードIDを追加
        }
        _handCardList.Sort(); // 手札をカードID順にソート
    }

    // ここのリストに関する操作を実装
    // メソッドの命名規則はList_〇〇で統一
    public void List_Add(bool _side,int _num)
    {
        if(!_side) _handCardList.Add(_num);
        else _fieldCardList.Add(_num);
    }
    public void List_Remove(bool _side, int _num)
    {
        if(!_side) _handCardList.Remove(_num);
        else _fieldCardList.Remove(_num);
    }
    public int List_GetIndex(int _cardID)
    {
        int _index = _handCardList.IndexOf(_cardID);
        return _index;
    }
    public int List_GetValue(bool _side, int _index)
    {
        if(!_side && _handCardList.Count > 0) return _handCardList[_index];
        else if (_side && _fieldCardList.Count > 0) return _fieldCardList[_index];
        else return 0;
    }
    public int List_GetHandValue(int _index)
    {
        return _handCardList[_index];
    }
    public int List_GetFieldValue(int _index)
    {
        if (_fieldCardList != null && _fieldCardList.Count > 0) return _fieldCardList[_index];
        else return 0;
    }
    public int List_GetFieldCount()
    {
        return _fieldCardList.Count;
    }
}
