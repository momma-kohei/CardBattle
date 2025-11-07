using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] Image cardImage;
    // [SerializeField] TextMeshProUGUI descriptionText;

    public void Show(CardModel cardModel)
    {
        nameText.text = cardModel.cardName;
        powerText.text = cardModel.power.ToString();
        cardImage.sprite = cardModel.cardTexture;
        // descriptionText.text = cardModel.description.ToString(); 
    }
}
