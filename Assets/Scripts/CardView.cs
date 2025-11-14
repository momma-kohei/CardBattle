using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// データ情報をもとに画面に表示するクラス
/// </summary>
public class CardView : MonoBehaviour // data -> display
{
    // カードオブジェクトのUIと結びつけるための変数
    // variants -> UI
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

