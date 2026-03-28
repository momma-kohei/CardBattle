using ListModels;
using UnityEngine;

public class Battle_mng : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CalcDamage(Zone_mdl atkZoneM, Zone_mdl defZoneM)
    {
        int damage = CalcPower(atkZoneM, defZoneM) - defZoneM.GetArea().GetTotalPower(); // ダメージ計算式（例: 攻撃力 × 属性の相性 - 防御力）
        return Mathf.Max(damage, 0); // ダメージは0未満にならないようにする
    }

    public int CalcPower(Zone_mdl atkZoneM, Zone_mdl defZoneM)
    {
        return atkZoneM.GetArea().GetTotalPower() * GetWeakness(atkZoneM, defZoneM); // ダメージ計算式（例: 攻撃力 × 属性の相性）
    }

    int GetWeakness(Zone_mdl atkZoneM, Zone_mdl defZoneM)
    {
        Area_mdl atkArea = atkZoneM.GetArea();
        Area_mdl defArea = defZoneM.GetArea();

        EleType _attack = atkArea.GetEleType();
        EleType _defense = defArea.GetEleType();
        // 属性の相性を定義
        if (_attack == _defense) return 1; // 等倍
        else if ((_attack == EleType.Fire && _defense == EleType.Grass) ||
                 (_attack == EleType.Water && _defense == EleType.Fire) ||
                 (_attack == EleType.Grass && _defense == EleType.Water)) return 2; // 効果抜群
        else return 0; // 効果なし
    }
}
