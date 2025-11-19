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
            _inField = !_inField; // カードがフィールド内にあるかどうかのフラグを反転
            this.gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, 0); // ローカルY座標を0に戻す
        }
        else
        {
            if (_cardManager.distinctElement(int.Parse(this.name))) // フィールド内のカードと属性が一致する場合
            {
                _cardManager.GeneratePlayerFieldCard(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
                _inField = !_inField; // カードがフィールド内にあるかどうかのフラグを反転
                this.gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, 10, 0); // ローカルY座標を10に変更
            }
        }
    }
}
