using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenterButtonView : MonoBehaviour
{
    [SerializeField] Button _centerButton;
    [SerializeField] TextMeshProUGUI _centerButtonText;
    [SerializeField] Image _centerButonBGD;

    // 余裕があれば押せるか押せないかでボタンの色を変更する処理
    public void SetButtonText(bool on)
    {
        if(on)
        {
            _centerButton.interactable = true;
            _centerButtonText.text = "END";
        }
        else
        {
            _centerButton.interactable = false;
            _centerButtonText.text = "";
        }
    }
}
