using ListModels;
using UnityEngine;

/// <summary>
/// バトルに関する処理を持つクラス
/// </summary>
public class Battle_ctl : MonoBehaviour
{
    [SerializeField] Field_ctl _fieldC;

    Area_mdl _areaM1;
    Area_mdl _areaM2;

    public void Start()
    {
        
    }

    public int CalcDamage(bool _isMyAtk)
    {
        GetArea(); // 場の状態を取得
        if (_isMyAtk)
        {
            return Mathf.Max(GetAttack(true) - GetDefense(true), 0); // ダメージ計算式（例: 攻撃力 × 属性の相性 - 防御力）
        }
        else
        {
            return Mathf.Max(GetAttack(false) - GetDefense(false), 0); // ダメージ計算式（例: 攻撃力 × 属性の相性 - 防御力）
        }
    }

    public int GetAttack(bool _isMyAtk)
    {
        if (_isMyAtk)
        {
            return CalcPower(_areaM1, _areaM2);
        }
        else
        {
            return CalcPower(_areaM2, _areaM1);
        }      
    }


    int CalcPower(Area_mdl atkAreaM, Area_mdl defAreaM)
    {
        return GetPower(atkAreaM) * GetWeakness(atkAreaM, defAreaM); // ダメージ計算式（例: 攻撃力 × 属性の相性）
    }

    public int GetDefense(bool _isMyAtk)
    {
        if (_isMyAtk)
        {
            return GetPower(_areaM2);
        }
        else
        {
            return GetPower(_areaM1);
        }
    }

    public int GetPower(Area_mdl areaM)
    {
        return areaM.GetTotalPower();
    }

    int GetWeakness(Area_mdl atkAreaM, Area_mdl defAreaM)
    {
        EleType _atkEle = atkAreaM.GetEleType();
        EleType _defEle = defAreaM.GetEleType();
        // 属性の相性を定義
        if (_atkEle == _defEle) return 1; // 等倍
        else if ((_atkEle == EleType.Fire && _defEle == EleType.Grass) ||
                 (_atkEle == EleType.Water && _defEle == EleType.Fire) ||
                 (_atkEle == EleType.Grass && _defEle == EleType.Water)) return 2; // 効果抜群
        else return 0; // 効果なし
    }

    void GetArea()
    {
        _areaM1 = _fieldC?.GetArea1();
        _areaM2 = _fieldC?.GetArea2();
    }
}
