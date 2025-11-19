using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject fieldCard;
    [SerializeField] UIController playerPowerSum;

    private int _childCount;

    void Start()
    {
        _childCount = 0;
    }

    void Update()
    {
        if (_childCount != fieldCard.transform.childCount)
        {
            playerPowerSum.UpdatePlayerPowerSum();
            Debug.Log("Player Power Sum Updated");
        }
        _childCount = fieldCard.transform.childCount;
    }

    // PlayerFieldのカード枚数が変化したタイミングで呼ばれるメソッド
    // Battleボタンが押されたタイミングで呼ばれるメソッド
    // PowerSumに表示する値を更新するメソッド

}
