using System;
using TMPro;
using UnityEngine;

/// <summary>
/// バトルに関する処理を行うクラス
/// </summary>
public class Battle_mgr : MonoBehaviour
{
    Battle_mdl _battleM;
    [SerializeField] Battle_viw _battleV;
    [SerializeField] Field_mgr _fieldManager;

    int _maxHitPoint = 30;
    int _hitPoint1;
    int _hitPoint2;

    GamePhase _currentPhase;

    //bool _isOnline = false; // オンラインかどうかのフラグ（必要に応じて設定）
    bool _isMyAtk;
    public bool IsMyAtk {  set { _isMyAtk = value; } }

    float _openDelay = 2f; // 場札を公開してから次のフェーズに遷移するまでの時間
    float _cpuDelay = 0.5f; // CPUの行動を呼び出すまでの遅延時間

    public event Action SetEnemyNameAction;
    public event Action FillHands;
    public event Action EnemyAction;
    public event Action TransitionEvent;
    public event Action CoinToss;

    private void Awake()
    {
        _hitPoint1 = _maxHitPoint;
        _hitPoint2 = _maxHitPoint;

        _fieldManager.CardAction += OnP1CardClicked; // アクションを_fieldManagerへ伝える
    }

    void Start()
    {
        _battleV.CenterButtonEvent += TransitionEvent; // アクションを_battleVへ伝える

        SetEnemyNameAction.Invoke();

        _battleM = new Battle_mdl(_fieldManager.FieldM);
        FillHands.Invoke(); // 手札を満たす処理
        CoinToss.Invoke(); // 先攻後攻の決定
        SetPhase(GamePhase.SelectAtk);
    }

    public void SetEnemyName(string name)
    {
        _battleV.SetEnemyName(name);
    }

    public void ResetField()
    {
        _fieldManager.Trash(); // 場札をすべて捨てる
        _battleV.ShowPower(_fieldManager.AreaPower1, true); // パワー表示をリセット
        _battleV.ShowPower(_fieldManager.AreaPower2, false);
        FillHands.Invoke(); // 手札を満たす処理
    }

    void ReduceHitPoint(int damage, bool isMine)
    {
        if (isMine) _hitPoint1 -= damage;
        else _hitPoint2 -= damage;
    }

    public void SetPhase(GamePhase nextPhase)
    {
        _currentPhase = nextPhase;

        switch (nextPhase)
        {
            case GamePhase.SelectAtk:
                Select(true, _isMyAtk);
                break;

            case GamePhase.SelectDef:
                Select(false, _isMyAtk);
                break;

            case GamePhase.Battle:
                Battle(_isMyAtk);
                if (Finished()) SetPhase(GamePhase.Finish);
                else Invoke(nameof(Transition), _openDelay);
                break;

            case GamePhase.Finish:
                SetAction(false); // 操作不可
                _battleV.ShowGameSet(_hitPoint1, _hitPoint2); // バトル終了処理
                break;
        }
    }

    void Select(bool isAtk, bool myAtk)
    {
        if (isAtk) _battleV.ShowAttackIcon(myAtk);
        SetAction(!(isAtk ^ myAtk));
        if (!(isAtk ^ myAtk)) return;
        EnemyAction.Invoke();
    }

    public void Battle(bool myAtk)
    {
        SetAction(false); // 操作受付終了

        _fieldManager.OpenArea(); // 場札を公開
        _battleV.ShowPower(_fieldManager.AreaPower2, false); // 相手のパワーを公開
        ReduceHitPoint(_battleM.CalcDamage(myAtk), !myAtk); // HPの更新
        int hitPoint = !myAtk ? _hitPoint1 : _hitPoint2;
        _battleV.ShowHitPoint(hitPoint, !myAtk); // HP表示
        if (!Finished()) Invoke(nameof(ResetField), _openDelay);
    }

    public enum GamePhase
    {
        SelectAtk,
        SelectDef,
        Battle,
        Finish
    }

    public void Transition()
    {
        switch (_currentPhase)
        {
            case GamePhase.SelectAtk:
                SetPhase(GamePhase.SelectDef);
                break;
            case GamePhase.SelectDef:
                SetPhase(GamePhase.Battle);
                break;
            case GamePhase.Battle:
                _isMyAtk = !_isMyAtk;
                SetPhase(GamePhase.SelectAtk);
                break;
        }
    }

    public void CPUAction()
    {
        Invoke(nameof(CPU), _cpuDelay);
    }

    void CPU()
    {
        _fieldManager.CPUmove();
        Transition(); // 遷移
    }

    bool Finished()
    {
        if (_hitPoint1 <= 0 || _hitPoint2 <= 0) return true;
        else return false;
    }

    public void SetAction(bool on)
    {
        _battleV.SetHandPanel(!on); // 手札の表示/非表示
        _fieldManager.SetCardAction(on);
    }

    void OnP1CardClicked(Card_viw card) // toggle & Area表示
    {
        if (_fieldManager.Toggle(card.Model, true))
        {
            card.MoveCard(); // Hand移動
            card.PlaySound(); // 効果音再生

            _battleV.SetCenterButtonEvent(false); // センターボタンのクリックイベントを解除
            if (!_fieldManager.IsAreaEmpty(true))
            {
                _battleV.SetCenterButtonEvent(true); // センターボタンのクリックイベントを登録
            }
        }
        _battleV.ShowPower(_fieldManager.AreaPower1, true); // Area1のパワーを表示
    }
}
