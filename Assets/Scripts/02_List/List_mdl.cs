using System;
using System.Collections.Generic;

namespace ListModels
{
    /// <summary>
    /// 汎用的なカードリストのモデルクラス
    /// </summary>
    public class List_mdl // 各カードリストの継承元となるクラス
    {
        protected List<CardModel> _list; // カードのリスト

        public List_mdl() // コンストラクタ
        {
            _list = new List<CardModel>(); // カードのリストを初期化
        }

        public bool Add(CardModel card) // カードをリストに追加
        {
            if (!_list.Contains(card))
            {
                _list.Add(card);
                return true;
            }
            return false;
        }

        public bool Remove(CardModel card) // カードをリストから削除
        {
            if (_list.Contains(card))
            {
                _list.Remove(card);
                return true;
            }
            return false;
        }

        public List<CardModel> GetList() // カードのリストを取得
        {
            return _list;
        }

        public CardModel GetCard(int index) // カードのリストからインデックスでカードを取得
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

        void InitDeck() // 山札を初期化
        {
            for (int i = 10; i < 40; i++)
            {
                Add(new CardModel(i)); // カードを追加
            }
            Shuffle(); // 山札をシャッフル
        }

        void Shuffle() // 山札をシャッフル
        {
            Random rand = new Random();
            for (int i = _list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                CardModel temp = _list[i];
                _list[i] = _list[j];
                _list[j] = temp;
            }
        }

        public void Reload(Trash_mdl trash) // 捨て札から山札をリロード
        {
            if (_list.Count == 0)
            {
                _list = new List<CardModel>(trash.GetList());
                Shuffle(); // 山札をシャッフル
                trash.Clear(); // 捨て札をクリア
            }
        }
    }

    public class Trash_mdl : List_mdl // 捨て札クラス
    {
        public Trash_mdl() : base() // コンストラクタ
        {
        }

        public void Clear() // 捨て札をクリア
        {
            _list.Clear();
        }
    }

    public class Hand_mdl : List_mdl // 手札クラス
    {

        public Hand_mdl() : base() // コンストラクタ
        {
        }

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

        public int GetTotalPower() // 場札のカードの合計パワーを取得
        {
            int totalPower = 0;
            foreach (CardModel card in _list)
            {
                totalPower += card.GetPower();
            }
            return totalPower;
        }

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

