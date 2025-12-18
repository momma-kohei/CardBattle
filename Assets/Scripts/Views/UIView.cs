using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// カードを除く情報をUIとして画面に表示するクラス
/// </summary>
public class UIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI powerText1 = null;
    [SerializeField] TextMeshProUGUI powerText2 = null;
    [SerializeField] TextMeshProUGUI hpText1 = null;
    [SerializeField] TextMeshProUGUI hpText2 = null;
    [SerializeField] Image iconImage1 = null;
    [SerializeField] Image iconImage2 = null;
    UIPresenter _uiPresenter;

    int _maxHP = 30; // 最大体力

    public void Start()
    {
        _uiPresenter = new UIPresenter();
        // このあたりはplayerModelから情報を取得して初期化する想定
        ShowPower(true, 0); // プレイヤー１のパワーを初期化
        ShowPower(false, 0); // プレイヤー２のパワーを初期化
        ShowHP(true, _maxHP); // プレイヤー１の体力を初期化
        ShowHP(false, _maxHP); // プレイヤー２の体力を初期化
    }

    public void ShowPower(bool isP1, int _number) // 合計パワーを表示するメソッド
    {
        if (isP1) if (powerText1 != null) powerText1.text = _number.ToString();
        else if (powerText2 != null) powerText2.text = _number.ToString();
    }
    public void ShowHP(bool isP1, int _number) // 残り体力を表示するメソッド
    {
        if (isP1) if (hpText1 != null) hpText1.text = _number.ToString();
        else if (hpText2 != null) hpText2.text = _number.ToString();
    }
    public void ShowIcon(bool isP1, Sprite _iconSprite) // アイコンを表示するメソッド
    {
        if (isP1) if (iconImage1 != null) iconImage1.sprite = _iconSprite;
        else if (iconImage2 != null) iconImage2.sprite = _iconSprite;
    }
}
