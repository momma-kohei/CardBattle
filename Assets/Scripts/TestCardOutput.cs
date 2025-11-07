using UnityEngine;

public class TestCardOutput : MonoBehaviour
{
    public CardController cardController = null;
    [Header("ÉJÅ[ÉhID")]public int cardID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // cardController = GetComponent<CardController>();
        cardController.Init(cardID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
