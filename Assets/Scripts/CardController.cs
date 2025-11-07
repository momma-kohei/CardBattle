using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view; // データを表示
    CardModel model; // データを管理

    private void Awake()
    {
        view = GetComponent<CardView>(); // CardViewのインスタンスを取得
    }

    public void Init(int cardID)
    {
        model = new CardModel(cardID);
        view.Show(model);
    }
}
