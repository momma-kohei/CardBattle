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
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI powerText = null;
    [SerializeField] Image cardImage = null;
    // [SerializeField] TextMeshProUGUI descriptionText; // カードの説明は仮実装

    /// <summary>
    /// カードデータの情報をカードに反映するメソッド
    /// </summary>
    /// <param name="cardModel">カード情報を格納するコンストラクタ</param>
    public void Show(CardModel cardModel)
    {
        if (nameText != null) nameText.text = cardModel.cardName;
        if (powerText != null) powerText.text = cardModel.power.ToString();
        if (cardImage != null) cardImage.sprite = cardModel.cardTexture;
        // descriptionText.text = cardModel.description.ToString(); // カードの説明は仮実装
    }
}

