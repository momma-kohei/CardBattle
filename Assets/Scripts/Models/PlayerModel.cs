using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤー情報を管理するモデルクラス
/// </summary>
public class PlayerModel
{
    string _playerName; // プレイヤー名
    Image _playerIcon; // プレイヤーアイコン
    int _hitPoints; // ヒットポイント
    int _power; // 攻撃力

    int _maxHP = 30; // 最大体力

    public PlayerModel(string _playerName, Image _playerIcon)
    {
        this._playerName = _playerName;
        this._playerIcon = _playerIcon;
        this._hitPoints = _maxHP;
        this._power = 0;
    }
}
