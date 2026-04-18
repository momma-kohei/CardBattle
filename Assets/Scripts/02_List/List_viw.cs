using ListModels;
using UnityEngine;

public class List_viw : MonoBehaviour
{
    [SerializeField] Card_viw _cardPrefab; // カードプレハブの参照

    Transform _place; // 表示する場所
    public Transform Place { get { return _place; } }

    public void Awake()
    {
        if (_cardPrefab == null)
        {
            Debug.LogError("Card prefab is not assigned in the inspector.");
        }
        _place = transform; // このスクリプトがアタッチされているオブジェクトのTransformを表示場所として使用
    }

    /// <summary>
    /// カードリストを表示
    /// </summary>
    /// <param name="listM">表示するカードリストモデル</param>
    /// <param name="side">表示する面</param>
    public void Show(List_mdl listM, CardSide side)
    {
        foreach (Transform _oldcard in _place)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < listM.Count; i++)
        {
            Card_viw card = Instantiate(_cardPrefab, _place);
            card.ShowCard(listM.GetCard(i), side); // 指定された面で表示
        }
    }
}
