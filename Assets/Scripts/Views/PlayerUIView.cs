using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _powerText = null;
    [SerializeField] TextMeshProUGUI _hpText = null;
    [SerializeField] Image _iconImage = null;

    

    public void ShowPlayerData(PlayerModel _playerModel)
    {
        ShowPower(_playerModel);
        ShowHP(_playerModel);
        _iconImage.sprite = _playerModel.PlayerIcon.sprite;
    }

    public void ShowPower(PlayerModel _playerModel)
    {
        _powerText.text = _playerModel.GetPower().ToString();
    }

    public void ShowFinalPower(int _num)
    {
        _powerText.text = _num.ToString();
    }

    public void ShowHP(PlayerModel _playerModel)
    {
        _hpText.text = _playerModel.HitPoint.ToString();
    }
}
