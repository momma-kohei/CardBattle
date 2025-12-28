using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

/// <summary>
/// プレイヤー情報を管理するモデルクラス
/// </summary>
public class PlayerModel
{
    DeckModel _deckModel; // 共通のデッキのモデル

    // プレイヤー固有の情報
    string _playerName; // プレイヤー名
    Image _playerIcon; // プレイヤーアイコン    
    public string PlayerName { get { return _playerName; } }
    public Image PlayerIcon { get { return _playerIcon; } }

    // 定数情報
    int _maxHP = 30; // 最大体力
    int _handNum = 7; // 最大手札枚数

    // バトルに関する情報
    List<CardModel> _hand;
    List<CardModel> _area;
    public List<CardModel> Hand { get { return _hand; } }
    public List<CardModel> Area { get { return _area; } }
    int _hitPoint; // ヒットポイント
    bool _isAttacker; // 攻撃側か防御側か
    bool _isTurnFinished = false; // ターン終了フラグ
    public int HitPoint { get { return _hitPoint; } }
    public bool IsAttacker { get { return _isAttacker; } }
    public bool IsTurnFinished { get { return _isTurnFinished; } }

    public PlayerModel(DeckModel deckModel, string _playerName, Image _playerIcon)
    {
        Debug.Log("PlayerModelのコンストラクタが呼ばれました。");
        _deckModel = deckModel; // 共通のデッキモデルを受け取る

        // プレイヤー固有の情報の初期化
        this._playerName = _playerName;
        this._playerIcon = _playerIcon;

        // バトルに関する情報の初期化
        this._hitPoint = _maxHP;
        this._hand = new List<CardModel>();
        this._area = new List<CardModel>();
    }

    public void DrawCards()
    {
        while (this._hand.Count < _handNum)
            this._hand.Add(_deckModel.PushCard()); // デッキからカードを1枚引いて手札に追加
        this._hand.Sort((a, b) => a.cardID.CompareTo(b.cardID)); // 手札をカードID昇順にソート
    }

    public void TrashCards()
    {
        for (int i = 0; i < this._area.Count; i++)
            _deckModel.PullTrashCard(this._area[i]); // エリアのカードを捨て札に追加
        foreach (CardModel card in this._area)
            _hand.Remove(card); // 手札からエリアのカードを削除
        this._area.Clear(); // エリアをクリア
    }

    public void SelectCard(CardModel _cardModel)
    {
        int _handIndex = _hand.IndexOf(_cardModel); // 手札リストから選択されたカードのインデックスを取得
        // Debug.Log($"選択された手札のインデックス: {_handIndex}");
        if (_handIndex >= 0 && _handIndex < _hand.Count) // 指定されたインデックスが正当であることと属性一致を確認
        {
            if (_area.Count == 0) // 場札が空の場合
            {
                Debug.Log("未選択なので場札に追加します。");
                _area.Add(_hand[_handIndex]); // 未選択ならば場札に追加
                _cardModel.isSelected = true;
            }
            else if (_cardModel.element == _area[0].element) // 属性が一致する場合
            {
                if (_cardModel.isSelected)
                {
                    Debug.Log("選択済みなので場札から削除します。");
                    _area.Remove(_hand[_handIndex]); // 選択済みならば場札から削除
                    _cardModel.isSelected = false;
                }
                else
                {
                    Debug.Log("未選択なので場札に追加します。");
                    _area.Add(_hand[_handIndex]); // 未選択ならば場札に追加
                    _cardModel.isSelected = true;
                }
            }
            else
            {
                Debug.Log("選択したカードの属性が場札と一致しません。");
            }
        }
        else Debug.Log("手札のインデックスが不正です。");
    }
    public void CalculateDamege(PlayerModel _playerModel2) // ダメージ計算メソッド
    {
        int _power1 = this.GetPower();
        int _power2 = _playerModel2.GetPower();
        if (_isAttacker)
        {
            // _P2.CalculateDamege(this); // 自分が防御側の場合、相手に再度計算を依頼
        }
        else
        {
            int _def = (_power2 - _power1 < 0) ? 0 : _power2 - _power1;
            int _effectiveness = GetEffectiveness(_playerModel2.GetElement(), this.GetElement());
            int _damage = _def * _effectiveness;
            this._hitPoint -= _damage;
        }
    }

    public int GetPower()
    {
        int totalPower = 0;
        for (int i = 0; i < this._area.Count; i++)
            totalPower += this._area[i].power; // エリア内のカードのパワーを合計
        return totalPower;
    }

    ElementType GetElement()
    {
        if (this._area.Count == 0) throw new InvalidOperationException("エリアにカードがありません。");
        else return this._area[0].element; // エリア内の先頭カードの属性を返す
    }

    int GetEffectiveness(ElementType _attack, ElementType _defense)
    {
        if (_attack == _defense) return 1; // 等倍
        else if ((_attack == ElementType.Fire && _defense == ElementType.Grass) ||
                 (_attack == ElementType.Water && _defense == ElementType.Fire) ||
                 (_attack == ElementType.Grass && _defense == ElementType.Water)) return 2; // 効果抜群
        else return 0; // 効果なし
    }

    public void NextTurn()
    {
        TrashCards(); // 選択したカードを捨て札に移動
        DrawCards(); // 手札を補充
        _isAttacker = !_isAttacker; // 攻撃側・防御側を交代
        _isTurnFinished = false; // ターン終了フラグをリセット
    }

    public void DecideFirstAttacker(PlayerModel _playerModel1, PlayerModel _playerModel2)
    {
        int num = UnityEngine.Random.Range(0, 2);
        if (num == 0)
        {
            _playerModel1._isAttacker = true;
            _playerModel2._isAttacker = false;
        }
        else
        {
            _playerModel1._isAttacker = false;
            _playerModel2._isAttacker = true;
        }
    }

    public void TurnEnd()
    {
        _isTurnFinished = true; // ターン終了フラグを立てる
    }

    public void TurnRequest(PlayerModel _opponent)
    {
        if (this._isTurnFinished && _opponent._isTurnFinished)
        {
            CalculateDamege(_opponent); // ダメージ計算
            _opponent.CalculateDamege(this); // 相手にもダメージ計算を依頼
            NextTurn(); // 次のターンへ
            _opponent.NextTurn(); // 相手も次のターンへ
        }
        else
        {

        }
    }

    // BattleTest用メソッド
    public void PrintHand()
    {
        Debug.Log($"{_playerName}の手札:" + string.Join(", ", _hand.ConvertAll(card => $"{card.cardID}")));
    }
    public void PrintArea()
    {
        Debug.Log($"{_playerName}のエリア:" + string.Join(", ", _area.ConvertAll(card => $"{card.cardID}")));
    }

    public CardModel GetHandCard (int _index)
    {
        return this._hand[_index];
    }
}
