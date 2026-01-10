using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using UnityEngine;
using cardLists;
using player;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    TableModel _table;
    [SerializeField] TablePresenter _tablePresenter;
    [SerializeField] ButtonView _buttonView;

    // アイコン画像例
    [SerializeField] Sprite _playerIconImage1;
    [SerializeField] Sprite _playerIconImage2;

    PhaseState _phase;
    bool _isTurnEndButtonSet = false;
    bool _isHandCardSet = false;
    bool _isGameover = false;

    void Start()
    {
        _table = _tablePresenter.GetTableModel();

        Debug.Log("GameManager Start");

        // ゲーム開始
        _phase = PhaseState.Idle;
        SetButtonStart();
        _buttonView.OnPressTurnEnd -= SetButtonTurnEnd;
        _buttonView.OnPressTurnEnd += SetButtonTurnEnd;
        SetTurnEndButtonEvent();
    }

    private void Update()
    {
        PlayerModel player2 = _table.GetPlayer2();
        // ステートマシン
        switch (_phase)
        {
            case PhaseState.Idle:
                // 待機状態
                
                // プレイヤーの操作を待つ
                if (!_isTurnEndButtonSet)
                {
                    Debug.Log("待機フェーズ");
                    SetButtonNext();
                    _buttonView.OnPressTurnEnd -= SetButtonTurnEnd;
                    _buttonView.OnPressTurnEnd += SetButtonTurnEnd;
                    SetTurnEndButtonEvent();
                }
                break;

            case PhaseState.Attack1:
                // プレイヤーの操作を待つ
                if (!_isHandCardSet)
                {
                    _tablePresenter.SetMyCardEvent();
                    _isHandCardSet = true;
                }
                if (!_isTurnEndButtonSet)
                {
                    Debug.Log("A1フェーズ");
                    SetTurnEndButtonEvent();
                }
                break;

            case PhaseState.Defence2:

                // 対戦相手の操作
                Debug.Log("D2フェーズ");
                BattleSystem.CPUAction(player2); // CPUの操作

                PhaseFlow();
                break;

            case PhaseState.Attack2:

                //対戦相手の操作
                Debug.Log("A2フェーズ");
                BattleSystem.CPUAction(player2); // CPUの操作

                PhaseFlow();
                break;

            case PhaseState.Defence1:
                // プレイヤーの操作を待つ
                if (!_isHandCardSet)
                {
                    _tablePresenter.SetMyCardEvent();
                    _isHandCardSet = true;
                }
                if (!_isTurnEndButtonSet)
                {
                    Debug.Log("D1フェーズ");
                    SetTurnEndButtonEvent();
                }
                break;

            case PhaseState.Battle:
                // バトルの計算を行う
                Debug.Log("Battleフェーズ");
                PhaseFlow();
                break;

            case PhaseState.Gameover:
                if (!_isGameover)
                {
                    Debug.Log("Game Over!");
                    _buttonView.OnPressTurnEnd -= SetButtonTurnEnd;
                    SetButtonFinish();
                    _isGameover = true;
                }
                return;
        }
    }

    void OnTurnEndButtonPressed()
    {
        _tablePresenter.UnsetMyCardEvent();
        _isHandCardSet = false;
        UnsetTurnEndButtonEvent();
        PhaseFlow();
    }

    // フェーズメソッド
    void PhaseFlow()
    {
        switch (_phase)
        {
            case PhaseState.Idle:
                _phase = _tablePresenter.EndPhaseIdle();
                break;

            case PhaseState.Attack1:
                _phase = _tablePresenter.EndPhaseAtk(PhaseState.Attack1);
                break;

            case PhaseState.Attack2:
                _phase = _tablePresenter.EndPhaseAtk(PhaseState.Attack2);
                break;

            case PhaseState.Defence1:
                _phase = _tablePresenter.EndPhaseDef();
                break;

            case PhaseState.Defence2:
                _phase = _tablePresenter.EndPhaseDef();
                break;

            case PhaseState.Battle:
                _phase = _tablePresenter.EndPhaseBattle();
                break;
        }
    }

    // ボタンにクリックイベントを登録/解除
    void SetTurnEndButtonEvent()
    {
        _buttonView.OnPressTurnEnd -= OnTurnEndButtonPressed; // ２重にならないよう一旦解除
        _buttonView.OnPressTurnEnd += OnTurnEndButtonPressed;
        _isTurnEndButtonSet = true;
    }
    void UnsetTurnEndButtonEvent()
    {
        _buttonView.OnPressTurnEnd -= OnTurnEndButtonPressed;
        _isTurnEndButtonSet = false;
    }

    #region Change Button Name
    // Buttonの名前を変更するメソッド群
    void SetButtonStart()
    {
        _buttonView.SetButton("Start");
    }
    void SetButtonNext()
    {
        _buttonView.SetButton("Next");
    }
    void SetButtonTurnEnd()
    {
        _buttonView.SetButton("TurnEnd");
    }
    void SetButtonFinish()
    {
        _buttonView.SetButton("Finish");
    }
    #endregion
}

public enum PhaseState // ステート
{
    Attack1,
    Attack2,
    Defence1,
    Defence2,
    Battle,
    Idle,
    Gameover
}

