using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// カードを除く情報をUIとして画面に表示するクラス
/// </summary>
public class UIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _powerText1 = null;
    [SerializeField] TextMeshProUGUI _powerText2 = null;
    [SerializeField] TextMeshProUGUI _hpText1 = null;
    [SerializeField] TextMeshProUGUI _hpText2 = null;
    [SerializeField] Image _iconImage1 = null;
    [SerializeField] Image _iconImage2 = null;

    public event Action InitGameEvent; // ゲーム開始時の初期化イベント
    public event Action OnClickBattleButton; // Battleボタンが押されたときのイベント
    public event Action OnClickExitButton; // Exitボタンが押されたときのイベント
    public event Action OnClickInfoButton; // Infoボタンが押されたときのイベント

    public void Start()
    {
        InitGameEvent?.Invoke(); // ゲーム開始時の初期化イベントを発火
    }

    public void ShowPower(int _power1, int _power2) // 合計パワーを表示するメソッド
    {
        _powerText1.text = _power1.ToString();
        _powerText2.text = _power2.ToString();
    }
    public void ShowHP(int _hp1, int _hp2) // 残り体力を表示するメソッド
    {
        _hpText1.text = _hp1.ToString();
        _hpText2.text = _hp2.ToString();
    }
    public void ShowIcon(Sprite _icon1, Sprite _icon2) // アイコンを表示するメソッド
    {
        _iconImage1.sprite = _icon1;
        _iconImage2.sprite = _icon2;
    }
}
