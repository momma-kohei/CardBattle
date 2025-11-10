using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// カードを制御するクラス
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField] CardController handCardPrefab; // プレハブを取得
    [SerializeField] CardController fieldCardPrefab; // プレハブを取得
    [SerializeField] Transform PlayerHandTransform; // 手札のTransformを取得
    [SerializeField] Transform PlayerFieldTransform; // フィールドのTransfromを取得

    /// <summary>
    /// カードの生成を制御するメソッド
    /// </summary>
    /// <param name="hand">生成先の場所</param>
    void GenerateHandCard(int cardID)
    {
        CardController card = Instantiate(handCardPrefab, PlayerHandTransform, false); // handにカードを生成
        ReceiveEvent re = card.GetComponent<ReceiveEvent>(); // プレハブのインスタンス化後にコンポーネントを取得
        re.SetGameManager(this); // GameManagerの情報をプレハブに渡す
        card.InitHand(cardID);
    }
    /// <summary>
    /// カードIDを指定してカードを生成するメソッド
    /// </summary>
    /// <param name="cardID">カードID</param>
    public void GenerateFieldCard(int cardID)
    {
        CardController card = Instantiate(fieldCardPrefab, PlayerFieldTransform, false); // fieldにカードを生成
        card.InitField(cardID);
    }
    /// <summary>
    /// カード枚数を指定してランダムなカードを生成するメソッド
    /// </summary>
    /// <param name="hand">生成先の場所</param>
    /// <param name="num">カードの枚数</param>
    void GenerateRandomHandCard(Transform hand, int num)
    {
        CardController card = Instantiate(handCardPrefab, hand, false);
        // ランダムなカードIDを指定してカードを手札に生成する処理を行う
        // numによって反復回数を制御
        // デッキから引くようにカードが減っていく動きも必要
    }
}
