using cardLists;
using UnityEngine;

public class Hand1View : MonoBehaviour
{
    [SerializeField] CardView _cardPrefab; // カードプレハブの参照
    [SerializeField] Transform _transform; // 手札を表示するTransformの参照

    /// <summary>
    /// 自身の手札を表面で表示
    /// </summary>
    /// <param name="hand"></param>
    public void Show(Hand hand)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < hand.GetSize(); i++)
        {
            CardView card = Instantiate(_cardPrefab, _transform);
            card.ShowCardFront(hand.GetCard(i)); // 表面で表示
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
