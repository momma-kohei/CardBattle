using UnityEngine;

public class TestCardOutput : MonoBehaviour // testとしてカードを生成
{
    public CardController cardController = null;
    [Header("カードID")]public int cardID = 0; // カードIDは十の位が属性番号（1-3），一の位が序列番号（1-8）である2桁の整数

    void Start()
    {
        cardController.Init(cardID);
    }

    void Update()
    {
        
    }
}
