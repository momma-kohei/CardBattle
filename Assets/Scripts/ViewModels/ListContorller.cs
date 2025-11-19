using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour
{
    DeckModel _deckModel; // DeckModel02のインスタンス化
    HandModel _playerHandModel; // HandModelのインスタンス化
    HandModel _opponentHandModel; // HandModelのインスタンス化
    // 各インスタンスに対してプロパティを設定
    public DeckModel DeckModel { get { return _deckModel; } }
    public HandModel PlayerHandModel { get { return _playerHandModel; } }
    public HandModel OpponentHandModel { get { return _opponentHandModel; } }

    void Start()
    {
        _deckModel = new DeckModel();
        _playerHandModel = new HandModel();
        _opponentHandModel = new HandModel();
    }

    //private void Update()
    //{
    //    _playerHandModel.Update();
    //}

    public void SelectCard(HandModel _handModel, int _cardID)
    {

        _handModel.List_Add(true, _cardID); // 手札のカードを選択してフィールドに追加
        // _handModel.fieldCardList.Sort(); // フィールドのカードをソート　※ソートはなしで好きなカードをトップに置ける仕様もあり
    }
    public void UnselectCard(HandModel _handModel, int _cardID)
    {
        _handModel.List_Remove(true, _cardID); // 手札のカードを再選択してフィールドから削除
    }
    public int GetIndex(int _cardID)
    {
        return PlayerHandModel.List_GetIndex(_cardID);
    }
}
