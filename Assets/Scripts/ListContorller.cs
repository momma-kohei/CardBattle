using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour
{
    DeckModel deckModel; // DeckModel02のインスタンス化
    HandModel playerHandModel; // HandModelのインスタンス化
    HandModel opponentHandModel; // HandModelのインスタンス化
    // 各インスタンスに対してプロパティを設定
    public DeckModel DeckModel { get { return deckModel; } }
    public HandModel PlayerHandModel { get { return playerHandModel; } }
    public HandModel OpponentHandModel { get { return opponentHandModel; } }

    void Start()
    {
        deckModel = new DeckModel();
        playerHandModel = new HandModel();
        opponentHandModel = new HandModel();
    }

    public void SelectCard(HandModel handModel, int cardID)
    {
        handModel.FieldCardList.Add(cardID); // 手札のカードを選択してフィールドに追加
        // handModel.fieldCardList.Sort(); // フィールドのカードをソート　※ソートはなしで好きなカードをトップに置ける仕様もあり
    }
    public void UnselectCard(HandModel handModel, int cardID)
    {
        handModel.FieldCardList.Remove(cardID); // 手札のカードを再選択してフィールドから削除
    }

}
