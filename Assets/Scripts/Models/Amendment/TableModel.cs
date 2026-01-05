using cardLists;
using player;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TableModel
{
    PlayerModel_Re _p1;
    PlayerModel_Re _p2;
    Deck _deck;
    Sprite _icon1;
    Sprite _icon2;

    public TableModel() // コンストラクタ
    {
        _deck = new Deck();
        _p1 = new PlayerModel_Re("Player1", _icon1, _deck);
        _p2 = new PlayerModel_Re("Player2", _icon2, _deck);
    }

    public bool TurnStart() // ターン開始の準備
    {
        if (_p1.GetPlayerStatus().GetHitPoint() > 0 && _p2.GetPlayerStatus().GetHitPoint() > 0)
        {
            // 使用済みカードを処理
            _p1.Trash();
            _p2.Trash();
            // 攻守交替
            _p1.GetPlayerStatus().SwitchPhase();
            _p2.GetPlayerStatus().SwitchPhase();
            // 手札の補充
            _p1.HandFill();
            _p2.HandFill();
            return true;
        }
        return false;
    }

    public bool PhaseBattle()
    {
        // Viewの操作で両側の場札が出揃った後に呼ばれる
        if (PhaseAtk())
        {
            if (PhaseDef())
            {
                int _damage = BattleSystem.CalcDamage(GetAreaAtk(), GetAreaDef()); // ダメージを計算
                GetDefender().GetPlayerStatus().TakeDamage(_damage); // ダメージを反映
                return true;
            }
        }
        return false;
    }

    public PlayerModel_Re GetPlayer1()
    {
        return _p1;
    }

    public PlayerModel_Re GetPlayer2()
    {
        return _p2;
    }



    // ----------内部メソッド----------

    bool PhaseAtk()
    {
        // 場札を選択する処理はViewで完了している

        PlayerModel_Re _attacker = GetAttacker();
        if (_attacker.GetArea().GetSize() > 0)
        {
            return true;
        }
        return false;
    }

    bool PhaseDef()
    {
        // 場札を選択する処理はViewで完了している

        PlayerModel_Re _defender = GetDefender();
        if (_defender.GetArea().GetSize() > 0)
        {
            return true;
        }
        return false;
    }
    
    Area GetAreaAtk()
    {
        return GetAttacker().GetArea();
    }

    Area GetAreaDef()
    {
        return GetDefender().GetArea();
    }

    void CoinToss() // 先後を決定
    {
        BattleSystem.CoinToss(_p1, _p2);
    }

    PlayerModel_Re GetAttacker() // アタッカーを取得
    {
        if (_p1.GetPlayerStatus().GetIsAtk()) return _p1;
        else return _p2;
    }

    PlayerModel_Re GetDefender() // ディフェンダーを取得
    {
        if (!_p1.GetPlayerStatus().GetIsAtk()) return _p1;
        else return _p2;

    }
}
