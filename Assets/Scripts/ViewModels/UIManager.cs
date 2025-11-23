//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class UIManager : MonoBehaviour
//{
//    [SerializeField] GameObject fieldCard;
//    [SerializeField] UIController playerPowerSum;
//    [SerializeField] UIController opponentPowerSum;
//    [SerializeField] UIController playerHP;
//    [SerializeField] UIController opponentHP;
//    [SerializeField] Image playerIcon;
//    [SerializeField] Image opponentIcon;

//    private int _playerFieldChildCount;
//    private int _opponentFieldChildCount;

//    void Start()
//    {
//        _playerFieldChildCount = 0;
//        _opponentFieldChildCount = 0;
//    }

//    void Update()
//    {
//        if (_playerFieldChildCount != fieldCard.transform.childCount)
//        {
//            playerPowerSum.UpdatePlayerPowerSum();
//        }
//        _playerFieldChildCount = fieldCard.transform.childCount;
//        if (_opponentFieldChildCount != fieldCard.transform.childCount)
//        {
//            opponentPowerSum.UpdatePlayerPowerSum();
//        }
//        _opponentFieldChildCount = fieldCard.transform.childCount;
//    }

//    // PlayerFieldのカード枚数が変化したタイミングで呼ばれるメソッド
//    // Battleボタンが押されたタイミングで呼ばれるメソッド
//    // PowerSumに表示する値を更新するメソッド

//}
