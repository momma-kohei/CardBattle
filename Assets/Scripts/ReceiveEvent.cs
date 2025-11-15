using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiveEvent : MonoBehaviour
{
    GameManager _gameManager;
    CardManager _cardManager;
    CardManager02 _cardManager02;
    private bool _inField = false;

    public void SetCardManager(CardManager cm)
    {
        _cardManager = cm;
    }
    public void SetCardManager02(CardManager02 cm2)
    {
        _cardManager02 = cm2;
    }

    public void MyPointerDownUI() // このオブジェクト（カードプレハブ）がクリックされたときに呼ばれるメソッド
    {
        Debug.Log(this.name + "が押された");

        if(_inField)
        {
            //cardManager.RemoveFieldCard(int.Parse(this.name)); // フィールド内に作成したオブジェクトを削除
            //inField = cardManager.untiBoolByElement(inField, int.Parse(this.name));
            _cardManager02.RemoveFieldCard(int.Parse(this.name)); // フィールド内に作成したオブジェクトを削除
            _inField = _cardManager02.untiBoolByElement(_inField, int.Parse(this.name));
        }
        else
        {
            //cardManager.GenerateFieldCardByElement(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
            //inField = cardManager.untiBoolByElement(inField, int.Parse(this.name));
            _cardManager02.GenerateFieldCardByElement(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
            _inField = _cardManager02.untiBoolByElement(_inField, int.Parse(this.name));
        }
    }
}
