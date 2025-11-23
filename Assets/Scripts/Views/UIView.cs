using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 数値情報を画面に表示するクラス
/// </summary>
public class UIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText = null;
    [SerializeField] Image iconImage = null;

    public void ShowNumber(int _number) // 数値を表示するメソッド
    {
        if (numberText != null) numberText.text = _number.ToString();
    }
    public void ShowIcon(Sprite _iconSprite) // アイコンを表示するメソッド
    {
        if (iconImage != null) iconImage.sprite = _iconSprite;
    }
}
