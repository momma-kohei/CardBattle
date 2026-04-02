using UnityEngine;

/// <summary>
/// 実際のフィールド操作を進めるクラス
/// </summary>
public class Field_mgr : MonoBehaviour
{
    [SerializeField] Field_ctl _fieldC;
    [SerializeField] Field_viw _fieldV;
    int _maxHandSize = 7;

    public void FillHand1()
    {
        while (_fieldC?.GetHand1().GetSize() < _maxHandSize)
        {
            Draw();
        }
        _fieldV.ShowHand1(_fieldC.GetHand1());
    }

    public void Draw()
    {
        Card_mdl card = _fieldC?.Pop();
        if (card != null)
        {
            _fieldC.Draw1(card);

            // オンラインの処理はここに書く（？）
            // 相手側の_fieldC.Draw2(card); みたいな
        }
    }

    public bool Toggle(Card_mdl card)
    {
        // 場札と同属性のカードだけがトグルできるようにする
        if (_fieldC?.GetArea1().GetEleType() != EleType.None && card.GetEleType() != _fieldC?.GetArea1().GetEleType())
        {
            return false;
        }

        _fieldC?.Toggle1(card);
        _fieldV.ShowArea1(_fieldC.GetArea1());

        // オンラインの処理はここに書く（？）
        // 相手側の_fieldC.Toggle2(card);
        // 相手側の_fieldV.ShowArea2(_fieldC.GetArea2());
        // みたいな

        return true;
    }

    public void Trash()
    {
        _fieldC?.Trash();
        _fieldV.ShowArea1(_fieldC.GetArea1());
        _fieldV.ShowHand1(_fieldC.GetHand1());
        _fieldV.ShowArea2(_fieldC.GetArea2());
        _fieldV.ShowHand2(_fieldC.GetHand2());
    }

    public bool IsAreaEmpty()
    {
        return _fieldC?.GetArea1().GetSize() == 0;
    }

    public void OpenArea()
    {
        _fieldV?.OpenArea2(_fieldC.GetArea2());
    }
}
