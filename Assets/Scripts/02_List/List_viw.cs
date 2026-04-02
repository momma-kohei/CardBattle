using ListModels;
using UnityEngine;

public class List_viw : MonoBehaviour
{
    [SerializeField] Card_viw _cardPrefab; // カードプレハブの参照
    [SerializeField] Transform _transform; // 表示するTransformの参照
    [SerializeField] int _side; // 表示するリストの面（0:裏面、1:属性面、2:表面）

    public void Show(List_mdl listM)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < listM.GetSize(); i++)
        {
            Card_viw card = Instantiate(_cardPrefab, _transform);
            switch (_side)
            {
                case 0:
                    card.ShowCardBack(listM.GetCard(i)); // 裏面で表示
                    break;
                case 1:
                    card.ShowCardEle(listM.GetCard(i)); // 属性面で表示
                    break;
                case 2:
                    card.ShowCardFront(listM.GetCard(i)); // 表面で表示
                    break;
            }
        }
    }

    public void Open(List_mdl listM)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < listM.GetSize(); i++)
        {
            Card_viw card = Instantiate(_cardPrefab, _transform);
            card.ShowCardFront(listM.GetCard(i)); // 表面で表示
        }
    }

    public Transform GetTransform()
    {
        return _transform;
    }
}
