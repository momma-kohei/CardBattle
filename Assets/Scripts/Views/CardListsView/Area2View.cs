using cardLists;
using UnityEngine;

public class Area2View : MonoBehaviour
{
    [SerializeField] CardView _cardPrefab; // カードプレハブの参照
    [SerializeField] Transform _transform; // 手札を表示するTransformの参照

    /// <summary>
    /// 相手の場札を裏面で表示
    /// 表面は"Open"
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
            CardView card = Instantiate(_cardPrefab, _transform);
            card.ShowCardBack(area.GetCard(i)); // 裏面で表示
        }   
    }

    /// <summary>
    /// 相手の場札を表面で表示
    /// </summary>
    /// <param name="area"></param>
    public void Open(Area area)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < area.GetSize(); i++)
        {
            CardView card = Instantiate(_cardPrefab, _transform);
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
