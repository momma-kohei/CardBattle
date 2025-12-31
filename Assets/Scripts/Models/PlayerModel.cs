using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

/// <summary>
/// プレイヤー情報を管理するモデルクラス
/// </summary>
public class PlayerModel
{
    DeckModel _deckModel; // 共有する山札

    // プレイヤー固有の情報　※別のシーンで入力を受け付ける
    string _playerName; // プレイヤー名
    Image _playerIcon; // プレイヤーアイコン    
    public string PlayerName { get { return _playerName; } }
    public Image PlayerIcon { get { return _playerIcon; } }

    // 定数情報
    int _maxHP = 30; // 最大体力
    int _handNum = 7; // 最大手札枚数

    // バトルに関する情報
    List<CardModel> _hand; // 手札
    List<CardModel> _area; // 場札
    int _hitPoint; // ヒットポイント
    bool _isAttacker; // 攻撃側か防御側か
    public int HitPoint { get { return _hitPoint; } }
    public bool IsAttacker { get { return _isAttacker; } }
    public List<CardModel> Hand { get { return _hand; } }
    public List<CardModel> Area { get { return _area; } }

    public PlayerModel(DeckModel deckModel, string _playerName, Image _playerIcon) // コンストラクタ
    {
        //Debug.Log("PlayerModelのコンストラクタが呼ばれました。");

        _deckModel = deckModel; // 共有する山札のモデルを受け取る

        // プレイヤー固有の情報の初期化
        this._playerName = _playerName;
        this._playerIcon = _playerIcon;

        // バトルに関する情報の初期化
        this._hitPoint = _maxHP;
        this._hand = new List<CardModel>();
        this._area = new List<CardModel>();
    }

    /// <summary>
    /// デッキからカードを計７枚まで引き，手札をソートするメソッド
    /// </summary>
    public void DrawCards()
    {
        while (this._hand.Count < _handNum)
            this._hand.Add(_deckModel.PushCard()); // デッキからカードを1枚引いて手札に追加
        this._hand.Sort((a, b) => a.cardID.CompareTo(b.cardID)); // 手札をカードID昇順にソート
    }

    /// <summary>
    /// 選択してAreaにコピーしたカードをArea，Handから削除し捨て札に移動するメソッド
    /// </summary>
    public void TrashCards()
    {
        for (int i = 0; i < this._area.Count; i++)
            _deckModel.PullTrashCard(this._area[i]); // エリアのカードを捨て札に追加
        foreach (CardModel card in this._area)
            _hand.Remove(card); // 手札からエリアのカードを削除
        this._area.Clear(); // エリアをクリア
    }

    /// <summary>
    /// クリックしたカードを選択して場札に出す/戻すメソッド
    /// </summary>
    /// <param name="_cardModel">選択（クリック）されたカード</param>
    public void SelectCard(CardModel _cardModel)
    {
        int _handIndex = _hand.IndexOf(_cardModel); // 手札リストから選択されたカードのインデックスを取得
        // Debug.Log($"選択された手札のインデックス: {_handIndex}");
        if (_handIndex >= 0 && _handIndex < _hand.Count) // 指定されたインデックスが正当であることと属性一致を確認
        {
            if (_area.Count == 0) // 場札が空の場合
            {
                //Debug.Log("未選択なので場札に追加します。");
                _area.Add(_hand[_handIndex]); // 未選択ならば場札に追加
                _cardModel.isSelected = true;
            }
            else if (_cardModel.element == _area[0].element) // 属性が一致する場合
            {
                if (_cardModel.isSelected)
                {
                    //Debug.Log("選択済みなので場札から削除します。");
                    _area.Remove(_hand[_handIndex]); // 選択済みならば場札から削除
                    _cardModel.isSelected = false;
                }
                else
                {
                    //Debug.Log("未選択なので場札に追加します。");
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

    /// <summary>
    /// PlayerModelのHPをダメージ分減少させるメソッド
    /// </summary>
    /// <param name="_damage">ダメージ</param>
    public void Damage(int _damage)
    {
        int tempHP = this._hitPoint - _damage;
        if (tempHP < 0) this._hitPoint = 0; // HPが0未満にならないようにする
        else this._hitPoint = tempHP;
    }

    /// <summary>
    /// 選択して場札に出したカードのパワーを合計して返すメソッド
    /// </summary>
    /// <returns>合計パワーの整数値</returns>
    public int GetPower()
    {
        int totalPower = 0;
        for (int i = 0; i < this._area.Count; i++)
            totalPower += this._area[i].power; // エリア内のカードのパワーを合計
        return totalPower;
    }

    /// <summary>
    /// 選択して場札に出したカードの属性を返すメソッド
    /// </summary>
    /// <returns>属性</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ElementType GetAreaElement()
    {
        if (this._area.Count == 0) throw new InvalidOperationException("エリアにカードがありません。");
        else return this._area[0].element; // エリア内の先頭カードの属性を返す
    }

    public void DecideFirstAttacker(PlayerModel _playerModel1, PlayerModel _playerModel2)
    {
        int num = UnityEngine.Random.Range(0, 2); // 先後をランダムに決定
        //int num = 0; // テスト用に固定
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

    public void CPUAction()
    {
        // CPUの行動を決定するロジックをここに実装する

        // CPU1
        SelectCard(_hand[0]);
    }

    /// <summary>
    /// PlayerModelに対するターン終了処理
    /// </summary>
    public void EndTurn()
    {
        TrashCards(); // AreaカードをTrashに移動
        DrawCards(); // DeckからHandにカードを補充
        _isAttacker = !_isAttacker; // 攻守交代
    }

    #region For BattleTest
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

    public void NextTurn()
    {
        TrashCards(); // 選択したカードを捨て札に移動
        // -> EndTurn
        DrawCards(); // 手札を補充
        // -> StartTurn ---- EndTurnの最後で処理すべきかも
        _isAttacker = !_isAttacker; // 攻撃側・防御側を交代
        // -> EndTurn
        //_isTurnFinished = false; // ターン終了フラグをリセット
        // -> EndTurn
    }
    #endregion
}
