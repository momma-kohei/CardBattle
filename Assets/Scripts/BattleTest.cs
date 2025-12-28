using UnityEngine;

// Modelの動作確認用クラス
public class BattleTest : MonoBehaviour
{
    // 必要なモデルのインスタンスを定義
    DeckModel _deck;
    PlayerModel _P1;
    PlayerModel _P2;
    bool _isP1Turn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("--------初期化処理--------");
        _deck = new DeckModel();
        _P1 = new PlayerModel(_deck, "Player 1", null);
        _P1.DrawCards();
        _P2 = new PlayerModel(_deck, "Player 2", null);
        _P2.DrawCards();
        _P1.DecideFirstAttacker(_P1, _P2);
        PrintAll();
        Debug.Log("---------手札選択---------");
        SelectP1Attack();
        PrintAll();
        Debug.Log("-------ダメージ計算-------");
        _P1.CalculateDamege(_P2);
        _P2.CalculateDamege(_P1);
        PrintHP();
        Debug.Log("--------次のターン--------");
        _P1.NextTurn();
        _P2.NextTurn();
        PrintAll();
        Debug.Log("---------手札選択---------");
        SelectP2Attack();
        PrintAll();
        Debug.Log("-------ダメージ計算-------");
        _P1.CalculateDamege(_P2);
        _P2.CalculateDamege(_P1);
        PrintHP();
    }

    void PrintAll()
    {
        _P1.PrintHand();
        _P2.PrintHand();
        _P1.PrintArea();
        _P2.PrintArea();
        _deck.PrintDeckNum();
    }

    void PrintHP()
    {
        Debug.Log($"{_P1.PlayerName}の残りHP: {_P1.HitPoint} / {_P2.PlayerName}の残りHP: {_P2.HitPoint}");
    }

    void SelectP1Attack()
    {
        // P1の選択はクリックで行う想定
        _P1.SelectCard(_P1.GetHandCard(0));
        _P1.SelectCard(_P1.GetHandCard(1));
        _P1.SelectCard(_P1.GetHandCard(2));
        _P1.SelectCard(_P1.GetHandCard(3));
        // P2の選択は通信先から受け取る想定
        _P2.SelectCard(_P2.GetHandCard(0));
        _P2.SelectCard(_P2.GetHandCard(1));
        _P2.SelectCard(_P2.GetHandCard(1)); // 同じカードを2回選択すると解除されることを確認
    }

    void SelectP2Attack() {
        // P1の選択はクリックで行う想定
        _P1.SelectCard(_P1.GetHandCard(0));
        _P1.SelectCard(_P1.GetHandCard(2));
        _P1.SelectCard(_P1.GetHandCard(2)); // 同じカードを2回選択すると解除されることを確認
        // P2の選択は通信先から受け取る想定
        _P2.SelectCard(_P2.GetHandCard(0));
        _P2.SelectCard(_P2.GetHandCard(1));
        _P2.SelectCard(_P2.GetHandCard(2));
        _P2.SelectCard(_P2.GetHandCard(3));
    }
}
