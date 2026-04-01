using UnityEngine;
using System;
using player;

namespace player
{
    public class PlayerModel
    {
        PlayerInfoModel _info; // プレイヤーの情報
        PlayerStatusModel _status; // プレイヤーの状態
        cardLists.Hand _hand; // 手札
        cardLists.Area _area; // 場札
        cardLists.Deck _deck; // 共通の山札

        public PlayerModel(string name, Sprite icon, cardLists.Deck deck) // コンストラクタ
        {
            _info = new PlayerInfoModel(name, icon);
            _status = new PlayerStatusModel();
            _deck = deck; // 山札は外部から取得
            _area = new cardLists.Area();
            _hand = new cardLists.Hand();
        }

        /// <summary>
        /// 最大枚数になるまで手札を補充
        /// </summary>
        public void HandFill()
        {
            int loop = 0;
            while (_hand.Add(_deck.Draw(_hand)))
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
        public void Trash()
        {
            int loop = 0;
            while (_area.GetSize() > 0)
            {
                if (!_deck.AddTrash(_area.GetCard(0))) throw new Exception("Area -> Trash");
                if (!_hand.Remove(_area.GetCard(0))) throw new Exception("Hand Remove");
                if (!_area.Remove(_area.GetCard(0))) throw new Exception("Area Remove");
                loop++;
                if (loop == 10) break; // 無限ループ回避用
            }
        }

        /// <summary>
        /// カードを場札に追加/削除
        /// </summary>
        /// <param name="card">対象のカード</param>
        public void Toggle(Card_mdl card)
        {
            if (!_area.Add(card)) // 場札への追加に挑戦
            {
                _area.Remove(card); // 失敗ならば場札からの削除に挑戦
            }
        }

        // 主にUI用のゲッター
        #region Getter
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
        #endregion
    }

    public class PlayerInfoModel // プレイヤー固有の情報を扱うクラス
    {
        string _name;
        Sprite _icon;

        public PlayerInfoModel(string name, Sprite icon) // コンストラクタ
        {
            _name = name;
            _icon = icon;
        }

        // システム画面やUI用のゲッターとセッター
        #region Getter & Setter
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
        #endregion
    }

    public class PlayerStatusModel // プレイヤーの状態を扱うクラス
    {
        const int _maxHP = 30; // 最大HP
        int _hitPoint; // 現在のHP
        bool _isAtk; // Attackerかどうか

        public PlayerStatusModel() // コンストラクタ
        {
            _hitPoint = _maxHP;
            _isAtk = false;
        }

        /// <summary>
        /// ダメージを受ける処理を行うメソッド
        /// </summary>
        /// <param name="damage">ダメージ数値</param>
        /// <returns>動作成功か</returns>
        public bool TakeDamage(int damage)
        {
            if (_hitPoint > 0 && !_isAtk) // HP正かつDefender
            {
                _hitPoint -= damage;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 攻守の切り替え
        /// </summary>
        public void SwitchPhase()
        {
            _isAtk = !_isAtk;
        }

        // UIや条件分岐用のゲッター
        #region Getter
        /// <summary>
        /// Attackerかどうかを取得
        /// </summary>
        /// <returns></returns>
        public bool GetIsAtk()
        {
            return _isAtk;
        }

        /// <summary>
        /// HPを取得
        /// </summary>
        /// <returns></returns>
        public int GetHitPoint()
        {
            return _hitPoint;
        }
        #endregion
    }
}

public static class BattleSystem // バトルシステムクラス
{
    /// <summary>
    /// 確率で先後を決定
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    public static void CoinToss(TableModel tableModel)
    {
        int num = UnityEngine.Random.Range(0, 2); // コイントス
        if (num == 0)
        {
            tableModel.GetPlayer1().GetPlayerStatus().SwitchPhase();
        }
        else
        {
            tableModel.GetPlayer2().GetPlayerStatus().SwitchPhase();
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

    public static void CPUAction(PlayerModel player)
    {
        player.Toggle(player.GetHand().GetCard(0)); // インデックス0のカードを選択する
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