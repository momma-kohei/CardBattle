using System;
using System.Collections.Generic;

namespace cardLists
{
    public class Deck // 山札クラス
    {
        List<CardInfoModel> _deck;
        List<CardInfoModel> _trash;

        public Deck() // コンストラクタ
        {
            _deck = new List<CardInfoModel>();
            InitDeck();
        }

        /// <summary>
        /// 山札からカードを１枚ドロー
        /// </summary>
        /// <returns></returns>
        public CardInfoModel Draw()
        {
            while (_deck.Count > 0)
            if (_deck.Count == 0) ReloadDeck(); // 山札が空の場合，リロード
            CardInfoModel drawn = _deck[0]; // 山札の一番上のカードを取得
            _deck.RemoveAt(0); // 取得したカードを山札から削除
            return drawn; // ドローしたカードを返す
        }

        /// <summary>
        /// 捨て札にカードを追加
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool AddTrash(CardInfoModel card)
        {
            if (!_trash.Contains(card))
            {
                _trash.Add(card);
                return true;
            }
            return false;
        }


        // ----------内部メソッド----------

        void InitDeck() // 山札を初期化
        {
            for (int i = 0; i < 40; i++)
            {
                _deck.Add(new CardInfoModel(i));
            }
            ShuffleDeck();
        }

        void ReloadDeck()
        {
            if (_deck.Count == 0 && _trash.Count > 0)
            {
                _deck = new List<CardInfoModel>(_trash);
                _trash.Clear();
            }
        }

        void ShuffleDeck() // 山札をシャッフル
        {
            for (int i = 0; i < _deck.Count; i++)
            {
                int _randomIndex = UnityEngine.Random.Range(0, _deck.Count);
                CardInfoModel _temp = _deck[i];
                _deck[i] = _deck[_randomIndex];
                _deck[_randomIndex] = _temp;
            }
        }
    }

    public class Hand // 手札クラス
    {
        List<CardInfoModel> _hand;
        public const int maxSize = 7;

        public Hand() // コンストラクタ
        {
            _hand = new List<CardInfoModel>();
        }

        /// <summary>
        /// 手札の枚数を取得
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return _hand.Count;
        }

        /// <summary>
        /// 手札にカードを追加
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool Add(CardInfoModel card)
        {
            if (_hand.Count < maxSize)
            {
                _hand.Add(card);
                _hand.Sort((a, b) => a.GetID().CompareTo(b.GetID())); // 手札をカードID昇順にソート
                return true;
            }
            return false;
        }

        /// <summary>
        /// 手札からカードを削除
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool Remove(CardInfoModel card)
        {
            if (_hand.Contains(card))
            {
                _hand.Remove(card);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 手札からカードを取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CardInfoModel GetCard(int index)
        {
            if (index >=  0 && index < _hand.Count)
            {
                return _hand[index];
            }
            throw new ArgumentOutOfRangeException("hand index");
        }
    }

    public class Area // 場札クラス
    {
        List<CardInfoModel> _area;

        public Area() // コンストラクタ
        {
            _area = new List<CardInfoModel>();
        }

        /// <summary>
        /// 場札の枚数を取得
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return _area.Count;
        }

        /// <summary>
        /// 場札にカードを追加
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool Add(CardInfoModel card)
        {
            if (!_area.Contains(card))
            {
                if (_area.Count == 0) // 場札が空
                {
                    _area.Add(card);
                    return true;
                }
                else if (_area.Count > 0) // 場札が非空
                {
                    if (card.GetEleType() == GetEleType()) // 属性一致
                    {
                        _area.Add(card);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 場札からカードを削除
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool Remove(CardInfoModel card)
        {
            if (_area.Contains(card))
            {
                _area.Remove(card);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 場札の属性を取得
        /// </summary>
        /// <returns></returns>
        public EleType GetEleType()
        {
            if (_area.Count > 0)
            {
                return _area[0].GetEleType();
            }
            return EleType.None;
        }

        /// <summary>
        /// 場札の合計パワーを取得
        /// </summary>
        /// <returns></returns>
        public int GetPowerSum()
        {
            int sum = 0;
            for (int i = 0; i < _area.Count; i++)
            {
                sum += _area[i].GetPower();
            }
            return sum;
        }

        /// <summary>
        /// 場札からカードを取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CardInfoModel GetCard(int index)
        {
            if (index >= 0 && index < _area.Count)
            {
                return _area[index];
            }
            throw new ArgumentOutOfRangeException("area index");
        }
    }
}
