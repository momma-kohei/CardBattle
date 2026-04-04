using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenterButtonView : MonoBehaviour
{
    [SerializeField] Button TurnEndButton;
    [SerializeField] TextMeshProUGUI TurnEndText;

    public event Action OnPressTurnEnd;

    public void OnTurnEndButtonPressed()
    {
        OnPressTurnEnd?.Invoke();
    }

    // 余裕があれば押せるか押せないかでボタンの色を変更する処理
    public void SetButton(string text)
    {
        TurnEndText.text = text;
    }
}
