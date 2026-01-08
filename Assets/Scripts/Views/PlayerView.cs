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

    public PlayerView(PlayerModel player) // コンストラクタ
    {
        if (_power != null) ShowPower(player);
        if (_hitPoint != null) ShowHitPoint(player);
        //if (_name != null) _name.text = player.GetPlayerInfo().GetName();
        if (_icon != null) _icon.sprite = player.GetPlayerInfo().GetIcon();
    }

    /// <summary>
    /// プレイヤーのパワーを表示
    /// </summary>
    /// <param name="player"></param>
    public void ShowPower(PlayerModel player)
    {
        _power.text = player.GetArea().GetPowerSum().ToString();
    }

    /// <summary>
    /// プレイヤーのHPを表示
    /// </summary>
    /// <param name="player"></param>
    public void ShowHitPoint(PlayerModel player)
    {
        _hitPoint.text = player.GetPlayerStatus().GetHitPoint().ToString();
    }

    // 余裕によってアイコンにもクリックイベントを実装
}
