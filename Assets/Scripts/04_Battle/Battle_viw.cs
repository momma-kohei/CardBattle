using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// バトルに関する数値情報等を表示するクラス
/// </summary>
public class Battle_viw : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _hitPoint1Text;
    [SerializeField] TextMeshProUGUI _hitPoint2Text;
    [SerializeField] TextMeshProUGUI _power1Text;
    [SerializeField] TextMeshProUGUI _power2Text;
    [SerializeField] Image _atkIcon1;
    [SerializeField] Image _atkIcon1mini;
    [SerializeField] Image _atkIcon2;
    [SerializeField] Image _atkIcon2mini;
    [SerializeField] Sprite _atkIcon;
    [SerializeField] Sprite _defIcon;

    public void ShowHitPoint1(int hitPoint1)
    {
        _hitPoint1Text.text = hitPoint1.ToString();
    }

    public void ShowHitPoint2(int hitPoint2)
    {
        _hitPoint2Text.text = hitPoint2.ToString();
    }

    public void ShowPower1(int power1)
    {
        _power1Text.text = power1.ToString();
    }

    public void ShowPower2(int power2)
    {
        if (power2 == 0)
        {
            _power2Text.text = "?"; // パワーが0のときは「?」を表示する
        }
        else
        {
            _power2Text.text = power2.ToString();
        }
    }

    public void ShowAttack1()
    {
        _atkIcon1.sprite = _atkIcon; // 攻撃側のアイコンを攻撃アイコンにする
        _atkIcon1mini.sprite = _atkIcon; // 攻撃側のミニアイコンを攻撃アイコンにする

        _atkIcon2.sprite = _defIcon; // 防御側のアイコンを防御アイコンにする
        _atkIcon2mini.sprite = _defIcon; // 防御側のミニアイコンを防御アイコンにする
    }

    public void ShowAttack2()
    {
        _atkIcon1.sprite = _defIcon; // 防御側のアイコンを防御アイコンにする
        _atkIcon1mini.sprite = _defIcon; // 防御側のミニアイコンを防御アイコンにする

        _atkIcon2.sprite = _atkIcon; // 攻撃側のアイコンを攻撃アイコンにする
        _atkIcon2mini.sprite = _atkIcon; // 攻撃側のミニアイコンを攻撃アイコンにする
    }
}
