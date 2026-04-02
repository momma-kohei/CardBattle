using UnityEngine;

/// <summary>
/// 実際のフィールド操作を進めるクラス
/// </summary>
public class Field_mgr : MonoBehaviour
{
    [SerializeField] Field_ctl _fieldC;
    [SerializeField] Field_viw _fieldV;
    int _maxHandSize = 7;

    bool _isOnline = false; // オンラインかどうかのフラグ（仮）

    public void FillHand1()
    {
        while (_fieldC?.Hand1.GetSize() < _maxHandSize)
        {
            Draw();
        }
        _fieldV.ShowHand1(_fieldC.Hand1);
    }

    public void Draw()
    {
        Card_mdl card = _fieldC?.Pop();
        if (card != null)
        {
            _fieldC.Draw1(card);

            if (_isOnline) // ----------------------------------------------------------------------------
            {
                // オンライン
                // (相手側)_fieldC.Draw2(card);
            }
        }
    }

    public bool Toggle(Card_mdl card)
    {
        // 場札と同属性のカードだけがトグルできるようにする
        if (_fieldC?.Area1.GetEleType() != EleType.None && card.GetEleType() != _fieldC?.Area1.GetEleType())
        {
            return false;
        }

        _fieldC?.Toggle1(card);
        _fieldV.ShowArea1(_fieldC.Area1);

        if (_isOnline) // ----------------------------------------------------------------------------
        {
            // オンライン
            // (相手側)_fieldC.Toggle2(card);
            // (相手側)_fieldV.ShowArea2(_fieldC.GetArea2());
        }

        return true;
    }

    public void Trash()
    {
        _fieldC?.Trash();
        _fieldV.ShowArea1(_fieldC.Area1);
        _fieldV.ShowHand1(_fieldC.Hand1);
        _fieldV.ShowArea2(_fieldC.Area2);
        _fieldV.ShowHand2(_fieldC.Hand2);
    }

    public bool IsAreaEmpty()
    {
        return _fieldC?.Area1.GetSize() == 0;
    }

    public void OpenArea()
    {
        _fieldV?.OpenArea2(_fieldC.Area2);
    }
}
