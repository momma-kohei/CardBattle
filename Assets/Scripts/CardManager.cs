using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// リストとUIを結びつけ制御するクラス
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField] CardController playerHandCardPrefab;       // プレハブを取得
    [SerializeField] CardController playerFieldCardPrefab;      // プレハブを取得
    [SerializeField] CardController opponentHandCardPrefab;     // プレハブを取得
    [SerializeField] CardController opponentFieldCardPrefab;    // プレハブを取得
    [SerializeField] Transform PlayerHandTransform;             // プレイヤーの手札のTransformを取得
    [SerializeField] Transform PlayerFieldTransform;            // プレイヤーのフィールドのTransfromを取得
    [SerializeField] Transform OpponentHandTransform;           // 対戦相手の手札のTransfromを取得
    [SerializeField] Transform OpponentFieldTransform;          // 対戦相手のフィールドのTransfromを取得
    [SerializeField] ListController listController;             // ListControllerのインスタンス化
    int _elementNum = -1; // 選択された属性番号を-1で初期化
    int _fieldCardNum = 0; // フィールドに出ているカードの枚数を0で初期化

    public void GeneratePlayerHand(Transform _handTransform, int _cardID) // プレイヤーの手札にカードUIを生成するメソッド
    {
        CardController card = Instantiate(playerHandCardPrefab, _handTransform, false); // playerHandにカードプレハブを生成
        ReceiveEvent re = card.GetComponent<ReceiveEvent>(); // クリックイベントを受け取るコンポーネントを取得
        re.SetCardManager(this); // GameManagerの情報をプレハブに渡す
        card.Init(_cardID);
    }
    public void GenerateOpponentHand(Transform _handTransform, int _cardID) // 対戦相手の手札にカードUIを生成するメソッド
    {
        CardController card = Instantiate(opponentHandCardPrefab, _handTransform, false); // opponentHandにカードプレハブを生成
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
            GeneratePlayerHand(PlayerHandTransform, _cardID); // 手札にカードを生成
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
            GenerateOpponentHand(OpponentHandTransform, _cardID); // 手札にカードを生成
        }
    }
    public void GeneratePlayerFieldCard(int cardID) // フィールとにカードを生成するメソッド
    {
        CardController card = Instantiate(playerFieldCardPrefab, PlayerFieldTransform, false); // playerFieldにカードを生成
        card.Init(cardID); // カードIDを渡して初期化
        _fieldCardNum++; // フィールドに出ているカードの枚数をカウント
        _elementNum = card.GetCardElement(); // カードの属性番号を取得
        // listController.SelectCard(listController.PlayerHandModel, cardID);
    }
    public void GenerateOpponentFieldCard(int cardID) // フィールとにカードを生成するメソッド
    {
        CardController card = Instantiate(playerFieldCardPrefab, PlayerFieldTransform, false); // opponentFieldにカードを生成
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
                GeneratePlayerFieldCard(cardID);
                break;
            case 0: // 火属性が選択されている場合
                if (cardID / 10 == 1) GeneratePlayerFieldCard(cardID);
                break;
            case 1: // 水属性が選択されている場合
                if (cardID / 10 == 2) GeneratePlayerFieldCard(cardID);
                break;
            case 2: // 木属性が選択されている場合
                if (cardID / 10 == 3) GeneratePlayerFieldCard(cardID);
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
        // listController.UnselectCard(listController.PlayerHandModel, cardID);
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

