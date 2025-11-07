using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardDisplay : MonoBehaviour
{
    public CardData cardData;

    public Image cardImage;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI nameText;
    // public TextMeshPro descriptionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.name = cardData.name;

        if(cardData != null)
        {
            cardImage.sprite = cardData.cardImage;
            powerText.text = cardData.power.ToString();
            nameText.text = cardData.cardName;
            // descriptionText.text = cardData.description;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
