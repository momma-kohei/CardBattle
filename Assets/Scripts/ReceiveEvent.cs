using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiveEvent : MonoBehaviour
{
    GameManager _gameManager;
    CardManager _cardManager;
    HandLayoutGroup _handLayoutGroup;
    ListController _listController;
    private bool _inField = false;

    public void Awake()
    {
         RectTransform rect = GetComponent<RectTransform>();
    }
    public void SetCardManager(CardManager cardManager)
    {
        _cardManager = cardManager;
    }
    public void SetLayoutGroup(HandLayoutGroup handLayoutGroup)
    {
        _handLayoutGroup = handLayoutGroup;
    }

    public void MyPointerDownUI() // このオブジェクト（カードプレハブ）がクリックされたときに呼ばれるメソッド
    {
        // Debug.Log(this.name + "が押された");

        if(_inField)
        {
            _cardManager.RemoveFieldCard(int.Parse(this.name)); // フィールド内に作成したオブジェクトを削除
            _inField = _cardManager.untiBoolByElement(_inField, int.Parse(this.name));
            this.gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, 0);
        }
        else
        {
            _cardManager.GenerateFieldCardByElement(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
            _inField = _cardManager.untiBoolByElement(_inField, int.Parse(this.name));
            Debug.Log(this.transform.localPosition);
            this.gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, 10, 0);
            Debug.Log(this.transform.localPosition);
        }
    }
}
