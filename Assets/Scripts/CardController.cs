using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view; // データを表示
    CardModel model; // データを管理

    private void Awake()
    {
        view = GetComponent<CardView>(); // CardViewのインスタンスを取得
    }

    /// <summary>
    /// カードIDを指定してカードを初期化するメソッド
    /// </summary>
    /// <param name="cardID">属性番号と序列番号によるカードID</param>
    public void Init(int cardID)
    {
        model = new CardModel(cardID);
        view.Show(model);
        this.name = cardID.ToString();
    }
}
