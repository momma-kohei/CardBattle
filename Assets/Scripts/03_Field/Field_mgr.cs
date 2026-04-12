using ListModels;
using System;
using UnityEngine;

/// <summary>
/// 実際のフィールド操作を進めるクラス
/// </summary>
public class Field_mgr : MonoBehaviour
{
    [SerializeField] Field_viw _fieldV;
    [SerializeField] int _maxHandSize = 7;
    public int MaxHandSize { get { return _maxHandSize; }}
    Field_mdl _fieldM;
    public Field_mdl FieldM { get { return _fieldM; } }
    public int AreaPower1 { get { return _fieldM?.Area1.Power ?? 0; } }
    public int AreaPower2 { get { return _fieldM?.Area2.Power ?? 0; } }

    public event Action CPUaction;
    public event Action<Card_viw> CardAction;
    public event Action<bool> FillHandAction;
    public event Func<Card_mdl, bool, bool> ToggleFunc;

    public void Awake()
    {
        _fieldM = new Field_mdl();
    }

    /// <summary>
    /// 手札を最大枚数まで補充
    /// </summary>
    /// <param name="isMine">自分の手札かどうか</param>
    public void FillHand(bool isMine)
    {
        FillHandAction.Invoke(isMine);
    }

    public Hand_mdl GetMyHand(bool isMine)
    {
        return isMine ? _fieldM?.Hand1 : _fieldM?.Hand2;
    }

    public Card_mdl Pop(bool isMine)
    {
        return _fieldM?.Pop(isMine);
    }

    public void Draw(Card_mdl card, bool isMine)
    {
        _fieldM.Draw(card, isMine);
    }

    public void ShowHand(Hand_mdl hand, bool isMine)
    {
        _fieldV.ShowHand(hand, isMine);
    }

    /// <summary>
    /// カードを場札に追加/削除
    /// </summary>
    /// <param name="card">対象のカード</param>
    /// <param name="isMine">自分の場札かどうか</param>
    /// <returns>操作の成功可否</returns>
    public bool Toggle(Card_mdl card, bool isMine)
    {
        return ToggleFunc.Invoke(card, isMine);
    }

    public void SubToggle(Card_mdl card, bool isMine)
    {
        Area_mdl area = isMine ? _fieldM?.Area1 : _fieldM?.Area2;

        _fieldM?.Toggle(card, isMine);
        _fieldV.ShowArea(area, isMine);
    }

    public Area_mdl GetMyArea(bool isMine)
    {
        return isMine ? _fieldM?.Area1 : _fieldM?.Area2;
    }

    public bool TypeCheck(Card_mdl card, Area_mdl area)
    {
        return area.Type != EleType.None && card.Type != area.Type;
    }

    public void SetCardAction(bool on)
    {
        foreach (Card_viw cardV in _fieldV.Hand1Transform.GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked -= CardAction;
            if (on) cardV.OnCardClicked += CardAction;
        }
        foreach (Card_viw cardV in _fieldV.Area1Transform.GetComponentsInChildren<Card_viw>())
        {
            cardV.OnCardClicked -= CardAction;
            if (on) cardV.OnCardClicked += CardAction;
        }
    }

    /// <summary>
    /// 各場札のカードをすべて各手札から削除して、場札からも削除する
    /// </summary>
    public void Trash()
    {
        _fieldM?.Trash();
        _fieldV.ShowArea(_fieldM.Area1, true);
        _fieldV.ShowArea(_fieldM.Area2, false);
    }

    /// <summary>
    /// 場札が空かどうかを確認
    /// </summary>
    /// <returns>場札が空かどうか</returns>
    public bool IsAreaEmpty(bool isMine)
    {
        Area_mdl area = isMine ? _fieldM.Area1 : _fieldM?.Area2;
        return area.Count == 0;
    }

    /// <summary>
    /// 場札を開示
    /// </summary>
    public void OpenArea()
    {
        _fieldV?.OpenArea(_fieldM.Area2);
    }

    /// <summary>
    /// CPUの動作を呼び出す
    /// </summary>
    public void CPUmove()
    {
        CPUaction.Invoke();
    }
}
