using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

namespace cardLists
{
    public class Deck // 山札クラス
    {
        List<CardModel> _deck;
        List<CardModel> _trash;

        public Deck() // コンストラクタ
        {
            _deck = new List<CardModel>(); // 山札を初期化
            InitDeck();

            _trash = new List<CardModel>(); // 捨て札を初期化
        }

        /// <summary>
        /// 山札からカードを１枚ドロー
        /// </summary>
        /// <returns>ドローしたカード</returns>
        public CardModel Draw(Hand hand)
        {
            if (_deck.Count == 0)
            {
                ReloadDeck(); // 山札が空の場合，リロード
            }
            CardModel drawn = _deck[0]; // 山札の一番上のカードを取得
            if (hand.Add(drawn)) // 取得したカードを手札に入れてみる
            {
                _deck.RemoveAt(0); // 取得したカードを山札から削除
            }
            hand.Remove(drawn); // 手札から削除
            return drawn;
        }

        /// <summary>
        /// 捨て札にカードを追加
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool AddTrash(CardModel card)
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
            for (int i = 10; i < 40; i++)
            {
                _deck.Add(new CardModel(i));
            }
            ShuffleDeck();
        }

        void ReloadDeck() // 捨て札から山札をリロード
        {
            if (_deck.Count == 0 && _trash.Count > 0)
            {
                //_deck.Clear();
                _deck = new List<CardModel>(_trash);
                ShuffleDeck();
                _trash.Clear();
            }
        }

        void ShuffleDeck() // 山札をシャッフル
        {
            for (int i = 0; i < _deck.Count; i++)
            {
                int _randomIndex = UnityEngine.Random.Range(0, _deck.Count);
                CardModel _temp = _deck[i];
                _deck[i] = _deck[_randomIndex];
                _deck[_randomIndex] = _temp;
            }
        }
    }

    public class Hand // 手札クラス
    {
        List<CardModel> _hand;
        public const int maxSize = 7; // 手札の最大枚数

        public Hand() // コンストラクタ
        {
            _hand = new List<CardModel>(); // 手札の初期化
        }

        /// <summary>
        /// 手札にカードを追加
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool Add(CardModel card)
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
        public bool Remove(CardModel card)
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
        public CardModel GetCard(int index)
        {
            if (index >=  0 && index < _hand.Count)
            {
                return _hand[index];
            }
            throw new ArgumentOutOfRangeException("hand index");
        }

        /// <summary>
        /// 手札の枚数を取得
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return _hand.Count;
        }
    }

    public class Area // 場札クラス
    {
        List<CardModel> _area;

        public Area() // コンストラクタ
        {
            _area = new List<CardModel>();
        }

        /// <summary>
        /// 場札にカードを追加
        /// </summary>
        /// <param name="card"></param>
        /// <returns>動作成功か</returns>
        public bool Add(CardModel card)
        {
            if (!_area.Contains(card)) // 追加済ではない
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
        public bool Remove(CardModel card)
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
        public CardModel GetCard(int index)
        {
            if (index >= 0 && index < _area.Count)
            {
                return _area[index];
            }
            throw new ArgumentOutOfRangeException("area index");
        }

        /// <summary>
        /// 場札の枚数を取得
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            if (_area != null)
            {
                return _area.Count;
            }
            throw new Exception("Area is null");
        }
    }
}
