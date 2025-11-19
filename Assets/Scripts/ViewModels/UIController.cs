using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    UIView _UIView;
    // [SerializeField] CardController fieldCardPrefab;
    [SerializeField] ListController listController;

    public void Awake()
    {
        if (GetComponent<UIView>() != null) _UIView = GetComponent<UIView>();
    }

    public void UpdatePlayerPowerSum()
    {
        UpdatePowerSum(listController.PlayerHandModel);
    }
    public void UpdatePowerSum(HandModel _handModel) // フィールドのカードのパワー合計を表示
    {
        int sum = 0;
        if (_handModel.List_GetFieldCount() > 0)
        {
            for (int i = 0; i < _handModel.List_GetFieldCount(); i++)
            {
                CardController _cardPrefab = (CardController)(GameObject.Find("/PlayCanvas/PlayerField/" + _handModel.List_GetValue(true, i))).GetComponent<CardController>();
                sum += _cardPrefab.GetCardPower();
            }
        }
        _UIView.ShowNumber(sum);
    }
}
