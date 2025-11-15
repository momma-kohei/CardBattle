using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiveEvent : MonoBehaviour
{
    GameManager _gameManager;
    CardManager _cardManager;
    private bool _inField = false;

    public void SetCardManager(CardManager cm)
    {
        _cardManager = cm;
    }

    public void MyPointerDownUI() // このオブジェクト（カードプレハブ）がクリックされたときに呼ばれるメソッド
    {
        Debug.Log(this.name + "が押された");

        if(_inField)
        {
            _cardManager.RemoveFieldCard(int.Parse(this.name)); // フィールド内に作成したオブジェクトを削除
            _inField = _cardManager.untiBoolByElement(_inField, int.Parse(this.name));
        }
        else
        {
            _cardManager.GenerateFieldCardByElement(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
            _inField = _cardManager.untiBoolByElement(_inField, int.Parse(this.name));
        }
    }
}
