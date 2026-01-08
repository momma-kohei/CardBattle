using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] Button TurnEndButton;
    [SerializeField] TextMeshProUGUI TurnEndText;
    [SerializeField] Button ExitButton;
    [SerializeField] Button InfoButton;

    public event Action OnPressTurnEnd;
    public event Action OnPressExit;
    public event Action OnPressInfo;

    public void OnTurnEndButtonPressed()
    {
        OnPressTurnEnd?.Invoke();
    }

    public void OnExitButtonPressed()
    {
        OnPressExit?.Invoke();
    }

    public void OnInfoButtonPressed()
    {
        OnPressInfo?.Invoke();
    }

    // 余裕があれば押せるか押せないかでボタンの色を変更する処理
    public void SetButton(string text)
    {
        TurnEndText.text = text;
    }
}
