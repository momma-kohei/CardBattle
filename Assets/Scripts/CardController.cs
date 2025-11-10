using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view; // データを表示する機能を取得
    CardModel model; // カードデータを取得するための器

    private void Awake()
    {
        view = GetComponent<CardView>(); // CardViewのインスタンスを取得
    }

    /// <summary>
    /// カードIDを指定してカードを初期化するメソッド
    /// </summary>
    /// <param name="cardID">属性番号と序列番号によるカードID</param>
    public void InitHand(int cardID)
    {
        model = new CardModel(cardID);
        view.Show(model);
        this.name = cardID.ToString();
    }
    public void InitField(int cardID)
    {
        model = new CardModel(cardID);
        view.Show(model);
        this.name = cardID.ToString();
    }
}
