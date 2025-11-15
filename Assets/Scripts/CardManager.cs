using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// カードを制御するクラス
/// </summary>
public class CardManager : MonoBehaviour
{
    DeckModel deckModel; // CardDeckModelのインスタンス化
    public List<int> deck; // デッキのカードIDリスト

    [SerializeField] CardController handCardPrefab; // プレハブを取得
    [SerializeField] CardController fieldCardPrefab; // プレハブを取得
    [SerializeField] Transform PlayerHandTransform; // 手札のTransformを取得
    [SerializeField] Transform PlayerFieldTransform; // フィールドのTransfromを取得
    int elementNum = -1; // 選択された属性番号
    int fieldCardNum = 0; // フィールドに出ているカードの枚数

    public void InitDeck() // デッキ情報を初期化するメソッド
    {
        deckModel = new DeckModel();
        deck = deckModel.deck; // デッキ情報を初期化
    }

    public void GenerateHandCard(int cardID) // 手札にカードを生成するメソッド
    {
        CardController card = Instantiate(handCardPrefab, PlayerHandTransform, false); // handにカードを生成
        ReceiveEvent re = card.GetComponent<ReceiveEvent>(); // プレハブのインスタンス化後にコンポーネントを取得
        re.SetCardManager(this); // GameManagerの情報をプレハブに渡す
        card.Init(cardID);
    }
    /// <summary>
    /// カード枚数を指定してランダムなカードを生成するメソッド
    /// </summary>
    /// <param name="hand">生成先の場所</param>
    /// <param name="num">カードの枚数</param>
    public void GenerateRandomHandCard(int num)
    {
        deckModel.PickCardFromDeck(num); // デッキからカードを引く処理を実行
        foreach (int cardID in deckModel.handCardList)
        {
            Debug.Log("HandCardID:" + cardID);
            GenerateHandCard(cardID); // 手札にカードを生成
        }
    }
    public void GenerateFieldCard(int cardID) // フィールとにカードを生成するメソッド
    {
        CardController card = Instantiate(fieldCardPrefab, PlayerFieldTransform, false); // fieldにカードを生成
        card.Init(cardID); // カードIDを渡して初期化
        fieldCardNum++; // フィールドに出ているカードの枚数をカウント
        elementNum = card.GetCardElement(); // カードの属性番号を取得
    }
    /// <summary>
    /// 属性を考慮してフィールドにカードを生成するメソッド
    /// </summary>
    /// <param name="cardID">カードID</param>
    public void GenerateFieldCardByElement(int cardID)
    {
        switch ((int)elementNum)
        {
            case -1: // 属性が選択されていない場合
                GenerateFieldCard(cardID);
                break;
            case 0: // 火属性が選択されている場合
                if (cardID / 10 == 1) GenerateFieldCard(cardID);
                break;
            case 1: // 水属性が選択されている場合
                if (cardID / 10 == 2) GenerateFieldCard(cardID);
                break;
            case 2: // 木属性が選択されている場合
                if (cardID / 10 == 3) GenerateFieldCard(cardID);
                break;
        }
    }
    /// <summary>
    /// フィールドのカードを消去するメソッド
    /// </summary>
    /// <param name="cardID">カードID</param>
    public void RemoveFieldCard(int cardID)
    {
        Destroy(GameObject.Find("/PlayCanvas/PlayerField/" + cardID)); // フィールド内に作成したオブジェクトを破壊
        fieldCardNum--; // フィールドに出ているカードの枚数をカウント
        if (fieldCardNum == 0) elementNum = -1; // 属性番号をリセット
    }
    /// <summary>
    /// 属性を考慮してbool値を反転させるメソッド
    /// </summary>
    /// <param name="b">対称のbool値</param>
    /// <param name="cardID">属性を判定するためのカードID</param>
    /// <returns></returns>
    public bool untiBoolByElement(bool b, int cardID)
    {
        switch ((int)elementNum)
        {
            case -1: // 属性が選択されていない場合
                b = !b;
                break;
            case 0: // 火属性が選択されている場合
                if (cardID / 10 == 1) b = !b;
                break;
            case 1: // 水属性が選択されている場合
                if (cardID / 10 == 2) b = !b;
                break;
            case 2: // 木属性が選択されている場合
                if (cardID / 10 == 3) b = !b;
                break;
        }
        return b;
    }
}

