using System;
using System.Collections.Generic;

namespace ListModels
{
    /// <summary>
    /// 汎用的なカードリストのモデルクラス
    /// </summary>
    public class List_mdl // 各カードリストの継承元となる
    {
        protected List<Card_mdl> _list; // カードのリスト
        public int Count { get { return _list.Count; } } // カードの枚数を取得するプロパティ

        public List_mdl() // コンストラクタ
        {
            _list = new List<Card_mdl>(); // カードのリストを初期化
        }

        /// <summary>
        /// カードリストへの追加操作
        /// </summary>
        /// <param name="card">対象のカードモデル</param>
        /// <returns>操作の成功可否</returns>
        public bool Add(Card_mdl card) // カードをリストに追加
        {
            if (!_list.Contains(card))
            {
                _list.Add(card);
                return true;
            }
            return false;
        }

        /// <summary>
        /// カードリストからの削除操作
        /// </summary>
        /// <param name="card">対象のカードモデル</param>
        /// <returns>操作の成功可否</returns>
        public bool Remove(Card_mdl card) // カードをリストから削除
        {
            if (_list.Contains(card))
            {
                _list.Remove(card);
                return true;
            }
            return false;
        }

        /// <summary>
        /// カードリストからインデックスでカードを取得する操作
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>インデックスに対応するカードモデル</returns>
        /// <exception cref="ArgumentOutOfRangeException">範囲外のインデックス</exception>
        public Card_mdl GetCard(int index) // カードのリストからインデックスでカードを取得
        {
            if (index >= 0 && index < Count)
            {
                return _list[index];
            }
            throw new ArgumentOutOfRangeException("null pointer exception about List");
        }
    }

    /// <summary>
    /// 山札のリストモデルクラス
    /// </summary>
    public class Deck_mdl : List_mdl
    {
        public Deck_mdl() : base() // コンストラクタ
        {
            InitDeck(); // 山札を初期化
        }

        /// <summary>
        /// 山札の一番上のカードを引く
        /// </summary>
        /// <returns>引いたカードモデル</returns>
        public Card_mdl Pop()
        {
            Card_mdl card;
            if (Count > 0)
            {
                card = _list[0];
                Remove(card);
            }
            else
            {
                card = null;
            }
            return card;
        }

        void InitDeck() // 山札を初期化
        {
            for (int i = 1; i <= 30; i++)
            {
                Add(new Card_mdl(i)); // カードを追加
            }
            Shuffle(); // 山札をシャッフル
        }

        void Shuffle() // 山札をシャッフル
        {
            Random rand = new Random();
            for (int i = _list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                Card_mdl temp = _list[i];
                _list[i] = _list[j];
                _list[j] = temp;
            }
        }
    }

    /// <summary>
    /// 手札のリストモデルクラス
    /// </summary>
    public class Hand_mdl : List_mdl
    {

        public Hand_mdl() : base() // コンストラクタ
        {
        }

        /// <summary>
        /// 手札をカードID昇順にソート
        /// </summary>
        public void Sort() // 手札をカードID昇順にソート
        {
            _list.Sort((a, b) => a.ID.CompareTo(b.ID));
        }
    }

    /// <summary>
    /// 場札のリストモデルクラス
    /// </summary>
    public class Area_mdl : List_mdl
    {
        public Area_mdl() : base() // コンストラクタ
        {
        }

        public int Power { get { return GetTotalPower(); } } // 場札のカードの合計パワーを取得するプロパティ
        public EleType Type { get { return GetEleType(); } } // 場札の属性を取得するプロパティ

        int GetTotalPower() // 場札のカードの合計パワーを取得
        {
            int totalPower = 0;
            foreach (Card_mdl card in _list)
            {
                totalPower += card.Power;
            }
            return totalPower;
        }

        EleType GetEleType() // 場札の属性を取得
        {
            if (_list.Count > 0)
            {
                return _list[0].Type;
            }
            return EleType.None;
        }
    }
}

