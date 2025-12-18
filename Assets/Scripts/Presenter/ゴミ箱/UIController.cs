//using TMPro;
//using UnityEngine;

//public class UIController : MonoBehaviour
//{
//    [SerializeField] ListController listController;

//    UIView _UIView;

//    private int _sum = 0;
//    public int Sum { get { return _sum; } }

//    public void Awake()
//    {
//        if (GetComponent<UIView>() != null) _UIView = GetComponent<UIView>();
//    }

//    public void UpdatePlayerPowerSum()
//    {
//        UpdatePowerSum(listController.PlayerHandModel);
//    }
//    public void UpdateOpponentPowerSum()
//    {
//        UpdatePowerSum(listController.OpponentHandModel);
//    }
//    public void UpdatePowerSum(HandModel _handModel) // フィールドのカードのパワー合計を表示
//    {
//        _sum = 0;
//        if (_handModel.List_GetFieldCount() > 0)
//        {
//            for (int i = 0; i < _handModel.List_GetFieldCount(); i++)
//            {
//                CardController _cardPrefab = (CardController)(GameObject.Find("/UICanvas/PlayerField/" + _handModel.List_GetValue(true, i))).GetComponent<CardController>();
//                _sum += _cardPrefab.GetCardPower();
//            }
//        }
//        _UIView.ShowNumber(_sum);
//    }
//    public void SetIcon(Sprite _iconSprite)
//    {
//        _UIView.ShowIcon(_iconSprite);
//    }
//}
