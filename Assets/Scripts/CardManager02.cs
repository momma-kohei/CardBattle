using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// カードを制御するクラス
/// </summary>
public class CardManager02 : MonoBehaviour
{
    [SerializeField] CardController handCardPrefab;     // プレハブを取得
    [SerializeField] CardController fieldCardPrefab;    // プレハブを取得
    [SerializeField] Transform PlayerHandTransform;     // プレイヤーの手札のTransformを取得
    [SerializeField] Transform PlayerFieldTransform;    // プレイヤーのフィールドのTransfromを取得
    [SerializeField] Transform OpponentHandTransform;   // 対戦相手の手札のTransfromを取得
    [SerializeField] Transform OpponentFieldTransform;  // 対戦相手のフィールドのTransfromを取得
    [SerializeField] ListController listController;     // FieldControllerのインスタンス化
    int _elementNum = -1; // 選択された属性番号を-1で初期化
    int _fieldCardNum = 0; // フィールドに出ているカードの枚数

    public void GenerateHandCard(Transform _handTransform, int _cardID) // UIとして手札にカードを生成するメソッド
    {
        CardController card = Instantiate(handCardPrefab, _handTransform, false); // handFieldにカードプレハブを生成
        ReceiveEvent re = card.GetComponent<ReceiveEvent>(); // プレハブのインスタンス化後にイベントを受け取るコンポーネントを取得
        re.SetCardManager02(this); // GameManagerの情報をプレハブに渡す
        card.Init(_cardID);
    }
    /// <summary>
    /// プレイヤーがデッキから指定枚数のカードを引くメソッド
    /// </summary>
    /// <param name="_num">引くカードの枚数</param>
    public void DrawPlayerHandRandom(int _num)
    {
        listController.PlayerHandModel.DrawCards(listController.DeckModel, _num); // デッキからカードを引く処理を実行
        foreach (int _cardID in listController.PlayerHandModel.HandCardList)
        {
            // Debug.Log("HandCardID:" + _cardID);
            GenerateHandCard(PlayerHandTransform, _cardID); // 手札にカードを生成
        }
    }
    /// <summary>
    /// 対戦相手がデッキから指定枚数のカードを引くメソッド
    /// </summary>
    /// <param name="_num">引くカードの枚数</param>
    public void DrawOpponentHandRandom(int _num)
    {
        listController.OpponentHandModel.DrawCards(listController.DeckModel, _num); // デッキからカードを引く処理を実行
        foreach (int _cardID in listController.OpponentHandModel.HandCardList)
        {
            Debug.Log("HandCardID:" + _cardID);
            GenerateHandCard(OpponentHandTransform, _cardID); // 手札にカードを生成
        }
    }
    public void GenerateFieldCard(int cardID) // フィールとにカードを生成するメソッド
    {
        CardController card = Instantiate(fieldCardPrefab, PlayerFieldTransform, false); // fieldにカードを生成
        card.Init(cardID); // カードIDを渡して初期化
        _fieldCardNum++; // フィールドに出ているカードの枚数をカウント
        _elementNum = card.GetCardElement(); // カードの属性番号を取得
    }
    /// <summary>
    /// 属性を考慮してフィールドにカードを生成するメソッド
    /// </summary>
    /// <param name="cardID">カードID</param>
    public void GenerateFieldCardByElement(int cardID)
    {
        switch ((int)_elementNum)
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
        _fieldCardNum--; // フィールドに出ているカードの枚数をカウント
        if (_fieldCardNum == 0) _elementNum = -1; // 属性番号をリセット
    }
    /// <summary>
    /// 属性を考慮してbool値を反転させるメソッド
    /// </summary>
    /// <param name="b">対称のbool値</param>
    /// <param name="cardID">属性を判定するためのカードID</param>
    /// <returns></returns>
    public bool untiBoolByElement(bool b, int cardID)
    {
        switch ((int)_elementNum)
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

