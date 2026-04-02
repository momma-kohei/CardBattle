using UnityEngine;

/// <summary>
/// 仮想的にCPUをプレイヤーとして動かすためのクラス
/// </summary>
public class CPU_mgr : MonoBehaviour
{
    [SerializeField] Field_ctl _fieldC;
    [SerializeField] Field_viw _fieldV;
    int _maxHandSize = 7;

    public void FillHand2()
    {
        while (_fieldC?.GetHand2().GetSize() < _maxHandSize)
        {
            Draw();
        }
        _fieldV.ShowHand2(_fieldC.GetHand2());
    }

    public void Draw()
    {
        Card_mdl card = _fieldC?.Pop();
        if (card != null)
        {
            _fieldC.Draw2(card);
        }
    }

    public void Toggle(Card_mdl card)
    {
        _fieldC?.Toggle2(card);
        _fieldV.ShowArea2(_fieldC.GetArea2());
    }
    public void CallActionCPU()
    {
        Toggle(_fieldC.GetHand2().GetCard(0));
    }
}
