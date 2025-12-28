using UnityEngine;
//using UnityEngine.EventSystems;
using System;

/// <summary>
/// カードを除く情報をUIとして画面に表示するクラス
/// </summary>
public class UIView : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI _powerText1 = null;
    //[SerializeField] TextMeshProUGUI _powerText2 = null;
    //[SerializeField] TextMeshProUGUI _hpText1 = null;
    //[SerializeField] TextMeshProUGUI _hpText2 = null;
    //[SerializeField] Image _iconImage1 = null;
    //[SerializeField] Image _iconImage2 = null;

    [SerializeField] GameObject _turnEndButton = null; // ターン終了ボタンのUIオブジェクト
    [SerializeField] GameObject _exitButton = null; // ゲーム終了ボタンのUIオブジェクト
    [SerializeField] GameObject _infoButton = null; // 情報表示ボタンのUIオブジェクト

    public event Action InitGameEvent; // ゲーム開始時の初期化イベント
    public event Action OnClickTurnEndButton; // Battleボタンが押されたときのイベント
    public event Action OnClickExitButton; // Exitボタンが押されたときのイベント
    public event Action OnClickInfoButton; // Infoボタンが押されたときのイベント

    public void Start()
    {
        InitGameEvent?.Invoke(); // ゲーム開始時の初期化イベントを発火
    }

    public void ClickTurnEndButton()
    {
        OnClickTurnEndButton?.Invoke(); // Battleボタンが押されたときのイベントを発火

        // Turn終了処理
        // Areaの同期
        // phaseの変更リクエスト
        //   _isTurnFinishedがtrueならばダメージ計算処理
        //   HPの同期
        //     Next Turn
    }
}
