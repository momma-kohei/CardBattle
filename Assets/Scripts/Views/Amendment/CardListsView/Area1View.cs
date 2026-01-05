using cardLists;
using UnityEngine;

public class Area1View : MonoBehaviour
{
    [SerializeField] CardInfoView _cardPrefab; // カードプレハブの参照
    [SerializeField] Transform _transform; // 手札を表示するTransformの参照

    public void Show(Area area)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < area.GetSize(); i++)
        {
            CardInfoView card = Instantiate(_cardPrefab, _transform);
            card.ShowCardFront(area.GetCard(i)); // 表面で表示
        }   
    }

    public Transform GetTransform()
    {
        return _transform;
    }
}
