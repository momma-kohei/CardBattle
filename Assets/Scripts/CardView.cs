using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{// カードオブジェクトのUIと結びつけるための変数
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] Image cardImage;
    // [SerializeField] TextMeshProUGUI descriptionText; // カードの説明は仮実装

    /// <summary>
    /// カードデータの情報をカードに反映するメソッド
    /// </summary>
    /// <param name="cardModel">カード情報を格納するコンストラクタ</param>
    public void Show(CardModel cardModel)
    {
        nameText.text = cardModel.cardName;
        powerText.text = cardModel.power.ToString();
        cardImage.sprite = cardModel.cardTexture;
        // descriptionText.text = cardModel.description.ToString(); // カードの説明は仮実装
    }
}
