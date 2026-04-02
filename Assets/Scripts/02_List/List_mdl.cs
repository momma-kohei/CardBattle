using System;
using System.Collections.Generic;

namespace ListModels
{
    /// <summary>
    /// 汎用的なカードリストのモデルクラス
    /// </summary>
    public class List_mdl // 各カードリストの継承元となるクラス
    {
        protected List<Card_mdl> _list; // カードのリスト

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

        // Get系のメソッド
        public List<Card_mdl> GetList() // カードのリストを取得
        {
            return _list;
        }

        public Card_mdl GetCard(int index) // カードのリストからインデックスでカードを取得
        {
            if (index >= 0 && index < GetSize())
            {
                return _list[index];
            }
            throw new ArgumentOutOfRangeException("null pointer exception about List");
        }

        public int GetSize() // カードの枚数を取得
        {
            return _list.Count;
        }
    }

    public class Deck_mdl : List_mdl // 山札クラス
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
            if (GetSize() > 0)
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
            for (int i = 10; i < 40; i++)
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

    public class Hand_mdl : List_mdl // 手札クラス
    {

        public Hand_mdl() : base() // コンストラクタ
        {
        }

        /// <summary>
        /// 手札をカードID昇順にソート
        /// </summary>
        public void Sort() // 手札をカードID昇順にソート
        {
            _list.Sort((a, b) => a.GetID().CompareTo(b.GetID()));
        }
    }

    public class Area_mdl : List_mdl // 場札クラス
    {
        public Area_mdl() : base() // コンストラクタ
        {
        }

        /// <summary>
        /// 場札のカードの合計パワーを取得
        /// </summary>
        /// <returns>合計値</returns>
        public int GetTotalPower() // 場札のカードの合計パワーを取得
        {
            int totalPower = 0;
            foreach (Card_mdl card in _list)
            {
                totalPower += card.GetPower();
            }
            return totalPower;
        }

        /// <summary>
        /// 場札のカードの属性を取得
        /// </summary>
        /// <returns>最初のカードの属性を、空の場合はNoneを返す</returns>
        public EleType GetEleType() // 場札の属性を取得
        {
            if (_list.Count > 0)
            {
                return _list[0].GetEleType();
            }
            return EleType.None;
        }
    }
}

