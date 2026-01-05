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

    public CardPresenter(PlayerModel pm1, PlayerModel pm2, PlayerCardView pcv1, PlayerCardView pcv2, PlayerUIView pUIv1, PlayerUIView pUIv2) // コンストラクタ
    {
        _playerModel1 = pm1;
        _playerModel2 = pm2;
        _playerCardView1 = pcv1;
        _playerCardView2 = pcv2;
        _playerUIView1 = pUIv1;
        _playerUIView2 = pUIv2;
    }

    void OnCardClicked(CardView _cardView)　// プレイヤー１がカードをクリックしたときのイベントハンドラ
    {
        _playerModel1.SelectCard(_cardView.CardModel); // Areaにカードを出す/戻す処理
        _playerUIView1.ShowPower(_playerModel1); // その都度Areaの合計攻撃力を表示
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // Areaを更新
        foreach (CardView _cv in _playerCardView1.AreaTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked += OnCardClicked; // 表示したAreaにもクリックイベントを登録

        // 手札を浮かせる処理を追加する場合はここに記述
    }

    // ゲーム進行に関するメソッド

    /// <summary>
    /// ゲーム開始メソッド：初期化処理と先手後手の確認を行い，アタッカーフェーズへ移行する
    /// </summary>
    public void StartGame() // ターン開始メソッド
    {
        // 両者の手札と場札を表示
        SetPlayerCards(_playerCardView1, _playerModel1, true);
        SetPlayerCards(_playerCardView2, _playerModel2, false);

        // 先手後手の確認
        PlayerModel _attacker = (_playerModel1.IsAttacker) ? _playerModel1 : _playerModel2;
        PlayerModel _defender = (_playerModel1.IsAttacker) ? _playerModel2 : _playerModel1;

        AttackerPhase(); // アタッカーフェーズへ移行
    }

    /// <summary>
    /// 選択されたカードからバトルの結果を反映するメソッド
    /// </summary>
    public void BattlePhase()
    {
        SetCardClickEvent(false); // クリックイベントを解除
        foreach (CardView _cv in _playerCardView1.AreaTransform.GetComponentsInChildren<CardView>()) _cv.OnClicked -= OnCardClicked; // Areaのクリックイベントを解除
        _playerCardView2.ShowCards(_playerModel2, "Area", 'F'); // プレイヤー２の場札を表示
        _playerUIView2.ShowPower(_playerModel2); // プレイヤー２の攻撃力を表示
        
        if (_playerModel1.IsAttacker)
        {
            CalculateDamage(_playerModel1, _playerModel2);
            _playerUIView1.ShowPowerNum(CalculatePower(_playerModel1, _playerModel2)); // プレイヤー１(Attacker)の攻撃力を表示
        }
        else
        {
            CalculateDamage(_playerModel2, _playerModel1);
            _playerUIView2.ShowPowerNum(CalculatePower(_playerModel2, _playerModel1)); // プレイヤー２(Attacker)の攻撃力を表示
        }
    }

    /// <summary>
    /// Attacker側の処理を行うメソッド
    /// </summary>
    public void AttackerPhase()
    {
        // StartPhase(); // フェーズ開始処理
        if ( _playerModel1.IsAttacker) SetCardClickEvent(true); // クリックイベントを登録
        else
        {
            // --------ここで対戦相手の操作--------
            _playerModel2.CPUAction(); // 仮で手札の先頭を選択
        }
        EndPhase(); // フェーズ終了処理
    }

    /// <summary>
    /// Defender側の処理を行うメソッド
    /// </summary>
    public void DefenderPhase()
    {
        // StartPhase(); // フェーズ開始処理
        if (_playerModel1.IsAttacker)
        {
            // --------ここで対戦相手の操作--------
            _playerModel2.CPUAction(); // 仮で手札の先頭を選択
        }
        else SetCardClickEvent(true); // クリックイベントを登録
        EndPhase(); // フェーズ終了処理
    }

    void EndPhase() // フェーズ終了メソッド
    {

        // --------ここでAreaを同期--------

        _playerCardView2.ShowCards(_playerModel2, "Area", 'B'); // プレイヤー２の場札を裏面で表示
    }

    /// <summary>
    /// ターン終了時の処理を行うメソッド
    /// </summary>
    public void EndTurn()
    {
        // 各PlayerModelに対してターン移行処理
        _playerModel1.EndTurn();
        _playerModel2.EndTurn();
        // 最終的な状態を開示
        _playerCardView1.ShowCards(_playerModel1, "Area", 'F'); // プレイヤー１の場札を表面で表示
        _playerCardView2.ShowCards(_playerModel2, "Area", 'F'); // プレイヤー１の場札を表面で表示
        _playerCardView1.ShowCards(_playerModel1, "Hand", 'F'); // プレイヤー１の手札を表面で表示
        _playerCardView2.ShowCards(_playerModel2, "Hand", 'E'); // プレイヤー２の手札を属性面で表示
    }

    // サブルーチン（このスクリプト内のコードを簡潔に書くためのメソッド群）

    void SetPlayerCards(PlayerCardView _playerCardView, PlayerModel _playerModel, bool _isMyPlayer) // 手札と場札を表示するメソッド
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

    void SetCardClickEvent(bool _on) // 手札に対してクリック判定を登録/解除するメソッド
    {
        if (_on) foreach (CardView _cardView in _playerCardView1.HandTransform.GetComponentsInChildren<CardView>()) _cardView.OnClicked += OnCardClicked; // クリックイベントを登録
        else foreach (CardView _cardView in _playerCardView1.HandTransform.GetComponentsInChildren<CardView>()) _cardView.OnClicked -= OnCardClicked; // クリックイベントを解除
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

    int GetEffectiveness(EleType _attack, EleType _defense) // 攻撃側と防御側の属性を比較し，効果を返すメソッド
    {
        if (_attack == _defense) return 1; // 等倍
        else if ((_attack == EleType.Fire && _defense == EleType.Grass) ||
                 (_attack == EleType.Water && _defense == EleType.Fire) ||
                 (_attack == EleType.Grass && _defense == EleType.Water)) return 2; // 効果抜群
        else return 0; // 効果なし
    }





    // --------カードを浮かせる処理の参考--------

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
