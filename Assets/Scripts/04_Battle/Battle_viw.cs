using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// バトルに関する数値情報等を表示するクラス
/// </summary>
public class Battle_viw : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _enemyName;
    // Battleの数値情報を表示するためのUI要素
    [SerializeField] TextMeshProUGUI _hitPoint1Text;
    [SerializeField] TextMeshProUGUI _hitPoint2Text;
    [SerializeField] TextMeshProUGUI _power1Text;
    [SerializeField] TextMeshProUGUI _power2Text;
    // 攻撃側と防御側を示すアイコン
    [SerializeField] UnityEngine.UI.Image _atkIcon1;
    [SerializeField] UnityEngine.UI.Image _atkIcon1mini;
    [SerializeField] UnityEngine.UI.Image _atkIcon2;
    [SerializeField] UnityEngine.UI.Image _atkIcon2mini;
    [SerializeField] Sprite _atkIcon;
    [SerializeField] Sprite _defIcon;
    // バトルの進行に用いるオブジェクト
    [SerializeField] GameObject _hand1Panel;
    [SerializeField] CenterButtonView _centerButtonV;
    [SerializeField] Result_viw _resultV;

    public event Action CenterButtonEvent;

    public void ShowHitPoint(int hitPoint, bool isMine)
    {
        TextMeshProUGUI hitPointText = isMine ? _hitPoint1Text : _hitPoint2Text;
        hitPointText.text = hitPoint.ToString();
    }

    public void ShowPower(int power, bool isMine)
    {
        TextMeshProUGUI powerText = isMine ? _power1Text : _power2Text;
        powerText.text = power.ToString();
        if (!isMine && power == 0) powerText.text = "?";
    }

    public void ShowAttackIcon(bool myAtk)
    {
        UnityEngine.UI.Image iconAtk = myAtk ? _atkIcon1 : _atkIcon2;
        UnityEngine.UI.Image miniAtk = myAtk ? _atkIcon1mini : _atkIcon2mini;
        UnityEngine.UI.Image iconDef = myAtk ? _atkIcon2 : _atkIcon1;
        UnityEngine.UI.Image miniDef = myAtk ? _atkIcon2mini : _atkIcon1mini;

        iconAtk.sprite = _atkIcon;
        miniAtk.sprite = _atkIcon;
        iconDef.sprite = _defIcon;
        miniDef.sprite = _defIcon;
    }

    public void SetCenterButtonEvent(bool on) // センターボタンに関するイベント
    {
        _centerButtonV.SetEvent(on);
        _centerButtonV.OnCenterButtonClicked -= CenterButtonEvent;
        if (on) _centerButtonV.OnCenterButtonClicked += CenterButtonEvent;
    }

    public void ShowGameSet(int hp1, int hp2)
    {
        _resultV.GameSet(hp1, hp2);
    }

    public void SetHandPanel(bool on)
    {
        _hand1Panel.SetActive(on);
    }

    public void SetEnemyName(string name)
    {
        _enemyName.text = "vs " + name;
    }
}
