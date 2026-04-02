using cardLists;
using player;
using UnityEngine;

public class TableModel // ゲームを行うテーブル全体のモデル
{
    PlayerModel _p1; // 自身のプレイヤーモデル
    PlayerModel _p2; // 相手のプレイヤーモデル
    Deck _deck; // 共通の山札
    Sprite _icon1;
    Sprite _icon2;

    public TableModel() // コンストラクタ
    {
        _deck = new Deck();
        _p1 = new PlayerModel("Player1", _icon1, _deck);
        _p2 = new PlayerModel("Player2", _icon2, _deck);
    }

    /// <summary>
    /// ターンを開始する処理
    /// </summary>
    /// <returns>動作成功か</returns>
    public bool StartTurn()
    {
        if (_p1.GetPlayerStatus().GetHitPoint() > 0 && _p2.GetPlayerStatus().GetHitPoint() > 0)
        {
            // 使用済みカードを捨て札へ
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

    /// <summary>
    /// 場札選択後の対戦処理
    /// </summary>
    public void Battle()
    {
        // Viewの操作で両側の場札が出揃った後に呼ばれる
        int _damage = BattleSystem.CalcDamage(GetAreaAtk(), GetAreaDef()); // ダメージを計算
        GetDefender().GetPlayerStatus().TakeDamage(_damage); // ダメージを反映
    }

    #region Getter
    /// <summary>
    /// プレイヤー１を取得
    /// </summary>
    /// <returns></returns>
    public PlayerModel GetPlayer1()
    {
        return _p1;
    }

    /// <summary>
    /// プレイヤー２を取得
    /// </summary>
    /// <returns></returns>
    public PlayerModel GetPlayer2()
    {
        return _p2;
    }

    /// <summary>
    /// Attackerを取得
    /// </summary>
    /// <returns></returns>
    public PlayerModel GetAttacker()
    {
        if (_p1.GetPlayerStatus().GetIsAtk()) return _p1;
        else return _p2;
    }

    /// <summary>
    /// Defenderを取得
    /// </summary>
    /// <returns></returns>
    public PlayerModel GetDefender() // ディフェンダーを取得
    {
        if (!_p1.GetPlayerStatus().GetIsAtk()) return _p1;
        else return _p2;

    }
    #endregion

    /// <summary>
    /// 場札が選択されているか
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CheckArea(PlayerModel player)
    {
        if (player.GetArea().GetSize() > 0)
        {
            return true;
        }
        return false;
    }

    // ----------内部メソッド----------

    Area GetAreaAtk()
    {
        return GetAttacker().GetArea();
    }

    Area GetAreaDef()
    {
        return GetDefender().GetArea();
    }

    
}
