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

    List<int> fieldCard = new List<int>();

    void Start()
    {
        deckModel = new DeckModel();
        playerHandModel = new HandModel();
        opponentHandModel = new HandModel();
    }

    public void SelectCard(HandModel handModel, int cardID)
    {
        Debug.Log("SelectCard is called!");
        fieldCard = new List<int>(handModel.FieldCardList);
        Debug.Log("New list is defined.");
        fieldCard.Add(cardID); // 手札のカードを選択してフィールドに追加
        handModel.FieldCardList = new List<int>(fieldCard);
        // handModel.fieldCardList.Sort(); // フィールドのカードをソート　※ソートはなしで好きなカードをトップに置ける仕様もあり
    }
    public void SelectPlayerHand(int cardID)
    {
        Debug.Log("SelectCard is called!");
        fieldCard = new List<int>(playerHandModel.FieldCardList);
        Debug.Log("New list is defined.");
        fieldCard.Add(cardID); // 手札のカードを選択してフィールドに追加
        Debug.Log("Card is added.");
        playerHandModel.FieldCardList = fieldCard;
        Debug.Log("Finish!");
        // handModel.fieldCardList.Sort(); // フィールドのカードをソート　※ソートはなしで好きなカードをトップに置ける仕様もあり
    }
    public void UnselectCard(HandModel handModel, int cardID)
    {
        handModel.FieldCardList.Remove(cardID); // 手札のカードを再選択してフィールドから削除
    }

}
