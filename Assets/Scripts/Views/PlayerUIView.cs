using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _powerText = null;
    [SerializeField] TextMeshProUGUI _hpText = null;
    [SerializeField] Image _iconImage = null;

    /// <summary>
    /// PlayerModelの情報をもとにパワーを表示するメソッド
    /// </summary>
    /// <param name="_playerModel">PlayerModel</param>
    public void ShowPower(PlayerModel _playerModel)
    {
        _powerText.text = _playerModel.GetPower().ToString();
    }

    /// <summary>
    /// 数値を指定してパワーを上書きするメソッド
    /// </summary>
    /// <param name="_num">数値</param>
    public void ShowPowerNum(int _num)
    {
        _powerText.text = _num.ToString();
    }

    /// <summary>
    /// PlayerModelの情報をもとにHPを表示するメソッド
    /// </summary>
    /// <param name="_playerModel">PlayerModel</param>
    public void ShowHP(PlayerModel _playerModel)
    {
        _hpText.text = _playerModel.HitPoint.ToString();
    }

    /// <summary>
    /// PlayerModelの情報をもとにアイコンを表示するメソッド
    /// </summary>
    /// <param name="_playerModel">PlayerModel</param>
    public void ShowIcon(PlayerModel _playerModel)
    {
        _iconImage.sprite = _playerModel.PlayerIcon.sprite;
    }
}
