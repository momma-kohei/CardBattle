using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiveEvent : MonoBehaviour
{
    GameManager gameManager;
    CardManager cardManager;
    private bool inField = false;

    /// <summary>
    /// カードマネージャーをセットするメソッド 
    /// </summary>
    /// <param name="cm"></param>
    public void SetCardManager(CardManager cm)
    {
        cardManager = cm;
    }

    public void MyPointerDownUI()
    {
        Debug.Log(this.name + "が押された");

        if(inField)
        {
            cardManager.RemoveFieldCard(int.Parse(this.name)); // フィールド内に作成したオブジェクトを削除
            inField = cardManager.untiBoolByElement(inField, int.Parse(this.name));
        }
        else
        {
            cardManager.GenerateFieldCardByElement(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
            inField = cardManager.untiBoolByElement(inField, int.Parse(this.name));
        }
    }
}
