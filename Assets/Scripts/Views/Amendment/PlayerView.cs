using player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _power = null;
    [SerializeField] TextMeshProUGUI _hitPoint = null;
    //[SerializeField] TextMeshProUGUI _name = null; // 未実装
    [SerializeField] Image _icon = null;

    public PlayerView(PlayerModel_Re player) // コンストラクタ
    {
        if (_power != null) ShowPower(player);
        if (_hitPoint != null) ShowHitPoint(player);
        //if (_name != null) _name.text = player.GetPlayerInfo().GetName();
        if (_icon != null) _icon.sprite = player.GetPlayerInfo().GetIcon();
    }

    public void ShowPower(PlayerModel_Re player)
    {
        _power.text = player.GetArea().GetPowerSum().ToString();
    }

    public void ShowHitPoint(PlayerModel_Re player)
    {
        _hitPoint.text = player.GetPlayerStatus().GetHitPoint().ToString();
    }

    // 余裕によってアイコンにもクリックイベントを実装
}
