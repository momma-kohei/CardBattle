using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// リストとUIを結びつけ制御するクラス
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField] CardController _playerHandCardPrefab;       // プレハブを取得
    [SerializeField] CardController _playerFieldCardPrefab;      // プレハブを取得
    [SerializeField] CardController _opponentHandCardPrefab;     // プレハブを取得
    [SerializeField] CardController _opponentFieldCardPrefab;    // プレハブを取得
    [SerializeField] Transform _playerHandTransform;             // プレイヤーの手札のTransformを取得
    [SerializeField] Transform _playerFieldTransform;            // プレイヤーのフィールドのTransfromを取得
    [SerializeField] Transform _opponentHandTransform;           // 対戦相手の手札のTransfromを取得
    [SerializeField] Transform _opponentFieldTransform;          // 対戦相手のフィールドのTransfromを取得
    [SerializeField] ListController _listController;             // ListControllerのインスタンス化

    int _elementNum = -1; // 選択された属性番号を-1で初期化
    int _fieldCardNum = 0; // フィールドに出ているカードの枚数を0で初期化

    public void GeneratePlayerHand(Transform _handTransform, int _cardID) // プレイヤーの手札にカードUIを生成するメソッド
    {
        CardController card = Instantiate(_playerHandCardPrefab, _handTransform, false); // playerHandにカードプレハブを生成
        ReceiveEvent re = card.GetComponent<ReceiveEvent>(); // クリックイベントを受け取るコンポーネントを取得
        re.SetCardManager(this); // GameManagerの情報をプレハブに渡す
        card.Init(_cardID);
    }
    public void GenerateOpponentHand(Transform _handTransform, int _cardID) // 対戦相手の手札にカードUIを生成するメソッド
    {
        CardController card = Instantiate(_opponentHandCardPrefab, _handTransform, false); // opponentHandにカードプレハブを生成
        card.Init(_cardID);
    }
    /// <summary>
    /// プレイヤーがデッキから指定枚数のカードを引くメソッド
    /// </summary>
    /// <param name="_num">引くカードの枚数</param>
    public void DrawPlayerHandRandom(int _num)
    {
        _listController.PlayerHandModel.DrawCards(_listController.DeckModel, _num); // デッキからカードを引く処理を実行
        foreach (int _cardID in _listController.PlayerHandModel.HandCardList)
        {
            // Debug.Log("HandCardID:" + _cardID);
            GeneratePlayerHand(_playerHandTransform, _cardID); // 手札にカードを生成
        }
    }
    /// <summary>
    /// 対戦相手がデッキから指定枚数のカードを引くメソッド
    /// </summary>
    /// <param name="_num">引くカードの枚数</param>
    public void DrawOpponentHandRandom(int _num)
    {
        _listController.OpponentHandModel.DrawCards(_listController.DeckModel, _num); // デッキからカードを引く処理を実行
        foreach (int _cardID in _listController.OpponentHandModel.HandCardList)
        {
            // Debug.Log("HandCardID:" + _cardID);
            GenerateOpponentHand(_opponentHandTransform, _cardID); // リストをもとに手札にカードを生成
        }
    }
    public void GeneratePlayerFieldCard(int _cardID) // フィールとにカードを生成するメソッド
    {
        CardController card = Instantiate(_playerFieldCardPrefab, _playerFieldTransform, false); // playerFieldにカードを生成
        card.Init(_cardID); // カードIDを渡して初期化
        _fieldCardNum++; // フィールドに出ているカードの枚数をカウント
        _elementNum = card.GetCardElement(); // カードの属性番号を取得
        _listController.SelectCard(_listController.PlayerHandModel, _cardID);
    }
    public void GenerateOpponentFieldCard(int _cardID) // フィールとにカードを生成するメソッド
    {
        CardController card = Instantiate(_playerFieldCardPrefab, _playerFieldTransform, false); // opponentFieldにカードを生成
        card.Init(_cardID); // カードIDを渡して初期化
        _fieldCardNum++; // フィールドに出ているカードの枚数をカウント
        _elementNum = card.GetCardElement(); // カードの属性番号を取得
    }

    public bool distinctElement(int _cardID) // 属性が一致するかどうかを判定するメソッド
    {
        bool b = false;
        switch ((int)_elementNum)
        {
            case -1: // 属性が選択されていない場合
                b = true;
                break;
            case 0: // 火属性が選択されている場合
                if (_cardID / 10 == 1) b = true;
                break;
            case 1: // 水属性が選択されている場合
                if (_cardID / 10 == 2) b = true;
                break;
            case 2: // 木属性が選択されている場合
                if (_cardID / 10 == 3) b = true;
                break;
        }
        return b;
    }
    /// <summary>
    /// フィールドのカードを消去するメソッド
    /// </summary>
    /// <param name="_cardID">カードID</param>
    public void RemoveFieldCard(int _cardID)
    {
        Destroy(GameObject.Find("/PlayCanvas/PlayerField/" + _cardID)); // フィールド内に作成したオブジェクトを破壊
        _listController.UnselectCard(_listController.PlayerHandModel, _cardID);
        _fieldCardNum--; // フィールドに出ているカードの枚数をカウント
        if (_fieldCardNum == 0) _elementNum = -1; // 属性番号をリセット
    }
}

