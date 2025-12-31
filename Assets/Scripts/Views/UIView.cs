using UnityEngine;
//using UnityEngine.EventSystems;
using System;

/// <summary>
/// カードを除く情報をUIとして画面に表示するクラス
/// </summary>
public class UIView : MonoBehaviour
{
    // ここいらないかも↓

    //[SerializeField] GameObject _turnEndButton = null; // ターン終了ボタンのUIオブジェクト
    //[SerializeField] GameObject _exitButton = null; // ゲーム終了ボタンのUIオブジェクト
    //[SerializeField] GameObject _infoButton = null; // 情報表示ボタンのUIオブジェクト
    //[SerializeField] GameObject _nextButton = null; // NextボタンのUIオブジェクト

    public event Action InitGameEvent; // ゲーム開始時の初期化イベント
    public event Action OnClickTurnEndButton; // Battleボタンが押されたときのイベント
    public event Action OnClickExitButton; // Exitボタンが押されたときのイベント
    public event Action OnClickInfoButton; // Infoボタンが押されたときのイベント

    public event Action OnClickNextButton; // Nextボタンが押されたときのイベント

    public void Start()
    {
        InitGameEvent?.Invoke(); // ゲーム開始時の初期化イベントを発火
    }

    public void ClickTurnEndButton()
    {
        OnClickTurnEndButton?.Invoke(); // Battleボタンが押されたときのイベントを発火

        // Phaseによって変化
        // Attacker -> 対戦相手のDefender Phaseへ
        // Defender -> Battle Phaseへ
    }

    public void ClickExitButton()
    {
        OnClickExitButton?.Invoke();
    }

    public void ClickInfoButton()
    {
        OnClickInfoButton?.Invoke();
    }

    public void ClickNextButton()
    {
        OnClickNextButton?.Invoke(); // Nextボタンが押されたときのイベントを発火
    }
}
