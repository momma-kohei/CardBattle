using UnityEngine;
using System;
using player;

namespace player
{
    public class PlayerModel_Re // -> PlayerModel
    {
        PlayerInfoModel _info;
        PlayerStatusModel _status;
        cardLists.Hand _hand;
        cardLists.Area _area;
        cardLists.Deck _deck;

        public PlayerModel_Re(string name, Sprite icon, cardLists.Deck deck) // コンストラクタ // -> PlayerModel
        {
            _info = new PlayerInfoModel(name, icon);
            _status = new PlayerStatusModel();
            _deck = deck;
        }

        /// <summary>
        /// 最大枚数になるまで手札を補充
        /// </summary>
        public void HandFill()
        {
            int loop = 0;
            while (_hand.Add(_deck.Draw()))
            {
                loop++;
                if (loop == 10) break; // 無限ループ回避用
            }
        }

        /// <summary>
        /// 場札と手札を捨て札に移動
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Trash()
        {
            while (_area.GetSize() > 0)
            {
                if (!_deck.AddTrash(_area.GetCard(0))) throw new Exception("Area -> Trash");
                if (!_hand.Remove(_area.GetCard(0))) throw new Exception("Hand Remove");
                if (!_area.Remove(_area.GetCard(0))) throw new Exception("Area Remove");
            }
            return true;
        }

        /// <summary>
        /// カードを場札に追加/削除
        /// </summary>
        /// <param name="card"></param>
        public void Toggle(CardInfoModel card)
        {

            if (!_area.Add(card)) // カードを場札に追加できなかった場合
            {
                _area.Remove(card); // 既存ならばカードを場札から削除
            }
        }

        /// <summary>
        /// 手札クラスを取得
        /// </summary>
        /// <returns></returns>
        public cardLists.Hand GetHand()
        {
            return _hand;
        }

        /// <summary>
        /// 場札クラスを取得
        /// </summary>
        /// <returns></returns>
        public cardLists.Area GetArea()
        {
            return _area;
        }

        /// <summary>
        /// プレイヤー情報を取得
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerInfo()
        {
            return _info;
        }

        /// <summary>
        /// プレイヤー状態を取得
        /// </summary>
        /// <returns></returns>
        public PlayerStatusModel GetPlayerStatus()
        {
            return _status;
        }
    }

    public class PlayerInfoModel // プレイヤー情報クラス
    {
        string _name;
        Sprite _icon;

        public PlayerInfoModel(string name, Sprite icon)
        {
            _name = name;
            _icon = icon;
        }

        /// <summary>
        /// 名前を変更
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 名前を取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// アイコンを変更
        /// </summary>
        /// <param name="icon"></param>
        public void SetIcon(Sprite icon)
        {
            _icon = icon;
        }

        /// <summary>
        /// アイコンを取得
        /// </summary>
        /// <returns></returns>
        public Sprite GetIcon()
        {
            return _icon;
        }
    }

    public class PlayerStatusModel // プレイヤー状態クラス
    {
        const int _maxHP = 30;
        int _hitPoint;
        bool _isAtk;

        public PlayerStatusModel()
        {
            _hitPoint = _maxHP;
            _isAtk = false;
        }

        public bool TakeDamage(int damage)
        {
            if (_hitPoint > 0 && !_isAtk)
            {
                _hitPoint -= damage;
                return true;
            }
            return false;
        }

        public void SwitchPhase()
        {
            _isAtk = !_isAtk;
        }

        public bool GetIsAtk()
        {
            return _isAtk;
        }

        public int GetHitPoint()
        {
            return _hitPoint;
        }
    }
}

public class BattleSystem // バトルシステムクラス
{
    /// <summary>
    /// 確率で先後を決定
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    public static void CoinToss(PlayerModel_Re p1, PlayerModel_Re p2)
    {
        int num = UnityEngine.Random.Range(0, 2); // コイントス
        if (num == 0)
        {
            p1.GetPlayerStatus().SwitchPhase();
        }
        else
        {
            p2.GetPlayerStatus().SwitchPhase();
        }
    }

    /// <summary>
    /// 場札の状態からバトルの処理を実行
    /// </summary>
    /// <param name="atkArea">攻撃側場札</param>
    /// <param name="defArea">防御側場札</param>
    /// <returns>防御側に与えられるダメージ</returns>
    public static int CalcDamage(cardLists.Area atkArea, cardLists.Area defArea) // クラスメソッド
    {
        int atk = atkArea.GetPowerSum();
        int def = defArea.GetPowerSum();
        EleType atkEle = atkArea.GetEleType();
        EleType defEle = defArea.GetEleType();

        int sub = atk * GetWeakness(atkEle, defEle) - def;

        if (sub > 0)
        {
            return sub;
        }
        return 0;
    }

    // ----------内部メソッド----------

    static int GetWeakness(EleType _attack, EleType _defense) // 防御側と属性を比較し，攻撃側の倍率を返すメソッド
    {
        if (_attack == _defense) return 1; // 等倍
        else if ((_attack == EleType.Fire && _defense == EleType.Grass) ||
                 (_attack == EleType.Water && _defense == EleType.Fire) ||
                 (_attack == EleType.Grass && _defense == EleType.Water)) return 2; // 効果抜群
        else return 0; // 効果なし
    }
}