using cardLists;
using UnityEngine;

public class Area1View : MonoBehaviour
{
    [SerializeField] Card_viw _cardPrefab; // カードプレハブの参照
    [SerializeField] Transform _transform; // 手札を表示するTransformの参照

    /// <summary>
    /// 自身の場札を表面で表示
    /// </summary>
    /// <param name="area"></param>
    public void Show(Area area)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < area.GetSize(); i++)
        {
            Card_viw card = Instantiate(_cardPrefab, _transform);
            card.ShowCardFront(area.GetCard(i)); // 表面で表示
        }   
    }

    /// <summary>
    /// Transformを取得
    /// </summary>
    /// <returns></returns>
    public Transform GetTransform()
    {
        return _transform;
    }
}
