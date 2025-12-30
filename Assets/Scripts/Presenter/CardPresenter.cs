using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CardPresenter
{
    PlayerModel _playerModel1; // プレイヤーモデルのインスタンスを保持する変数
    PlayerModel _playerModel2; // プレイヤーモデルのインスタンスを保持する変数
    PlayerCardView _playerCardView1; // PlayerCardViewのインスタンスを保持する変数
    PlayerCardView _playerCardView2; // PlayerCardViewのインスタンスを保持する変数
    PlayerUIView _playerUIView1;
    PlayerUIView _playerUIView2;

    // bool _selectedSameElement = false; // 同じ属性が選択されたかどうかを判定するフラグ

    public CardPresenter(PlayerModel playerModel1, PlayerModel playerModel2, PlayerCardView playerCardView1, PlayerCardView playerCardView2, PlayerUIView playerUIView1, PlayerUIView playerUIView2) // コンストラクタ
    {
        _playerModel1 = playerModel1;
        _playerModel2 = playerModel2;
        _playerCardView1 = playerCardView1;
        _playerCardView2 = playerCardView2;
        _playerUIView1 = playerUIView1;
        _playerUIView2 = playerUIView2;
    }

    public void OnCardClicked(CardView _cardView)
    {
        _playerModel1.SelectCard(_cardView.CardModel); // プレイヤー１の手札からエリアにカードを選択してコピー
        _playerUIView1.ShowPower(_playerModel1); // プレイヤー１の攻撃力を表示
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // プレイヤー１の場札を表示
        foreach (CardView _cv in _playerCardView1.AreaTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked += OnCardClicked; // Areaにクリックイベントを登録

        // 手札を浮かせる処理を追加する場合はここに記述
    }

    // ゲーム進行メソッド

    /// <summary>
    /// ターン開始メソッド：インスタンス化したPlayerModelの手札をPlaerCardViewを用いて表示する
    /// </summary>
    public void StartTurn() // ターン開始メソッド
    {
        // ボタンにイベントを登録
        SetPlayerHands(_playerCardView1, _playerModel1, true); // プレイヤー１の手札を表示
        //SetCardClickEvent(true); // クリックイベントを登録
        SetPlayerHands(_playerCardView2, _playerModel2, false); // プレイヤー２の手札を表示

        PlayerModel _attacker = (_playerModel1.IsAttacker) ? _playerModel1 : _playerModel2;
        PlayerModel _defender = (_playerModel1.IsAttacker) ? _playerModel2 : _playerModel1;

        AttackerPhase(); // アタッカーフェーズを準備
        //SetAttackerPhase(_attacker); // アタッカーフェーズを準備
        //SetDefenderPhase(_defender); // ディフェンダーフェーズを準備
    }

    // ボタンによって呼ばれるメソッド
    public void EndTurn() // ターン終了メソッド
    {
        SetCardClickEvent(false); // クリックイベントを解除
        // 両者のAreaを同期
        // 両者のAreaを表面で表示
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // プレイヤー１の場札を表示
        _playerCardView2.ShowCards(_playerModel2, "Area", 'F'); // プレイヤー２の場札を表示
        
        BattlePhase(); // バトルフェーズを実行
    }

    public void BattlePhase() // バトルフェーズの処理メソッド
    {
        SetCardClickEvent(false); // クリックイベントを解除
        foreach (CardView _cv in _playerCardView1.AreaTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked -= OnCardClicked; // Areaのクリックイベントを解除
        _playerCardView2.ShowCards(_playerModel2, "Area", 'F'); // プレイヤー２の場札を表示
        _playerUIView2.ShowPower(_playerModel2); // プレイヤー２の攻撃力を表示
        
        if (_playerModel1.IsAttacker)
        {
            CalculateDamage(_playerModel1, _playerModel2);
            _playerUIView1.ShowFinalPower(CalculatePower(_playerModel1, _playerModel2)); // プレイヤー１の攻撃力を表示
        }
        else
        {
            CalculateDamage(_playerModel2, _playerModel1);
            _playerUIView2.ShowFinalPower(CalculatePower(_playerModel2, _playerModel1)); // プレイヤー２の攻撃力を表示
        }
    }

    int CalculatePower(PlayerModel _attacker, PlayerModel _defender)
    {
        int _power1 = _attacker.GetPower();
        int _power2 = _defender.GetPower();
        int _effectiveness = GetEffectiveness(_attacker.GetAreaElement(), _defender.GetAreaElement());
        return _power1 * _effectiveness;
    }

    void CalculateDamage(PlayerModel _attacker, PlayerModel _defender) // ダメージ計算メソッド
    {
        // ダメージ計算処理
        int _power1 = CalculatePower(_attacker, _defender);
        int _power2 = _defender.GetPower();
        int _damage = (_power1 - _power2 < 0) ? 0 : _power1 - _power2;
        _defender.Damage(_damage);
    }

    int GetEffectiveness(ElementType _attack, ElementType _defense) // 攻撃側と防御側の属性を比較し，効果を返すメソッド
    {
        if (_attack == _defense) return 1; // 等倍
        else if ((_attack == ElementType.Fire && _defense == ElementType.Grass) ||
                 (_attack == ElementType.Water && _defense == ElementType.Fire) ||
                 (_attack == ElementType.Grass && _defense == ElementType.Water)) return 2; // 効果抜群
        else return 0; // 効果なし
    }

    public void AttackerPhase()
    {
        StartPhase();
        if ( _playerModel1.IsAttacker)
        {
            // ボタンへのEndPhaseイベントを登録
            SetCardClickEvent(true); // クリックイベントを登録
        }
        else
        {
            // 対戦相手の操作
            _playerModel2.SelectCard(_playerModel2.Hand[0]); // 仮で手札の先頭を選択
        }
        EndPhase();
    }

    public void DefenderPhase()
    {
        StartPhase();
        if (_playerModel1.IsAttacker)
        {
            // 対戦相手の操作
            _playerModel2.SelectCard(_playerModel2.Hand[0]); // 仮で手札の先頭を選択
        }
        else
        {
            // ボタンへのEndPhaseイベントを登録
            SetCardClickEvent(true); // クリックイベントを登録
        }
        EndPhase();
    }

    void StartPhase()
    {
        // 同期された相手のAreaを表示
        
        _playerCardView2.ShowCards(_playerModel2, "Area", 'B'); // プレイヤー２の場札を属性面で表示
    }

    // ボタンによって呼ばれるメソッド
    public void EndPhase()
    {
        //SetCardClickEvent(false); // クリックイベントを解除
        // ボタンのイベントを解除
        // Areaを同期
        _playerCardView2.ShowCards(_playerModel2, "Area", 'B'); // プレイヤー２の場札を裏面で表示
    }

    public void EndDefenderPhase()
    {
        SetCardClickEvent(false); // クリックイベントを解除
        // ボタンのイベントを解除
        // Areaを同期
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // プレイヤー１の場札を表面で表示
    }

    // サブルーチン
    void SetPlayerHands(PlayerCardView _playerCardView, PlayerModel _playerModel, bool _isMyPlayer)
    {
        char _areaState;
        char _handState;

        if (_isMyPlayer) {
            _areaState = 'F';
            _handState = 'F';
        }
        else {
            _areaState = 'B';
            _handState = 'E';
        }

        _playerModel.DrawCards(); // プレイヤーの手札を補充
        _playerCardView.ShowCards(_playerModel, "Area", _areaState); // プレイヤーの場札を更新
        _playerCardView.ShowCards(_playerModel, "Hand", _handState); // プレイヤーの手札を表示
    }

    void SetCardClickEvent(bool _on)
    {
        if (_on) foreach (CardView _cardView in _playerCardView1.HandTransform.GetComponentsInChildren<CardView>()) _cardView.OnClicked += OnCardClicked; // クリックイベントを登録
        else foreach (CardView _cardView in _playerCardView1.HandTransform.GetComponentsInChildren<CardView>()) _cardView.OnClicked -= OnCardClicked; // クリックイベントを解除
    }


    public void PrintHP()
    {
        Debug.Log($"{_playerModel1.PlayerName} HP: {_playerModel1.HitPoint}");
        Debug.Log($"{_playerModel2.PlayerName} HP: {_playerModel2.HitPoint}");
    }

    public void NextTurn()
    {
        // ターン交代処理
        _playerModel1.EndTurn();
        _playerModel2.EndTurn();
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // プレイヤー１の場札を裏面で表示
        _playerCardView2.ShowCards(_playerModel2, "Area", 'F'); // プレイヤー１の場札を裏面で表示
        _playerCardView1.ShowCards(_playerModel1, "Hand", 'F'); // プレイヤー１の手札を表示
        _playerCardView2.ShowCards(_playerModel2, "Hand", 'E'); // プレイヤー２の手札を表示
    }

    //public void NextTurn()
    //{
    //    _cardListModel.ReloadHand(true); // プレイヤー１の手札をリロード
    //    _cardListModel.ReloadHand(false); // プレイヤー２の手札をリロード
    //    _cardListView.ShowHand(_cardListModel); // 両者の手札を表示
    //    _selectedSameElement = false; // 同じ属性が選択されたかどうかのフラグをリセット
    //}
    //// クリックイベント
    //public void CompareElement(CardView _cardView) // クリックされたカードとareaList[0]の属性を比較するメソッド
    //{
    //    if (_cardListModel.Area1.Count == 0 || _cardView.CardModel.element == _cardListModel.Area1[0].element) _selectedSameElement = true;
    //    else _selectedSameElement = false;
    //}
    //public void SelectHandCardByP1Click(CardView _cardView) // プレイヤー１の手札から選択してエリアにコピーし選択している手札を浮かせるメソッド
    //{
    //    _cardListModel.SelectHandCard(_cardView.CardModel, _selectedSameElement, true);
    //    _cardListView.ShowArea1(_cardListModel);
    //    this.FloatSelectedP1Hand();
    //}
    //public void FloatSelectedP1Hand() // プレイヤー１の手札で選択されているカードを浮かせるメソッド
    //{
    //    if (_cardListModel.Area1.Count > 0)
    //    {
    //        foreach (CardModel _handCardModel in _cardListModel.Hand1)
    //        {
    //            foreach (CardModel _areaCardModel in _cardListModel.Area1)
    //            {
    //                if (_handCardModel == _areaCardModel)
    //                {
    //                    _cardListView.MoveHand1(_handCardModel, true);
    //                    break;
    //                }
    //                else _cardListView.MoveHand1(_handCardModel, false);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (CardModel _handCardModel in _cardListModel.Hand1)
    //        {
    //            _cardListView.MoveHand1(_handCardModel, false);
    //        }
    //        Debug.Log("Area1のリストにカードがありません。");
    //    }
    //}
}
