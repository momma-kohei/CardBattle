using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerCardView _playerCardView;
    [SerializeField] PlayerCardView _opponentCardView;
    [SerializeField] Image _playerIcon1;
    [SerializeField] Image _playerIcon2;
    DeckModel _deckModel;
    PlayerModel _playerModel1;
    PlayerModel _playerModel2;
    CardPresenter _cardPresenter;

    bool _oneIsMe = true;
    bool _oneIsFirstAttacker = true;

    void Start()
    {
        // モデルとプレゼンターの初期化
        Debug.Log("GameManager Start");
        _deckModel = new DeckModel();
        _playerModel1 = new PlayerModel(_deckModel, "Player1", _playerIcon1, _oneIsFirstAttacker);

        _playerModel2 = new PlayerModel(_deckModel, "Player2", _playerIcon2, !_oneIsFirstAttacker);
        _cardPresenter = new CardPresenter(_playerModel1, _playerModel2, _playerCardView, _opponentCardView);

        _cardPresenter.StartTurn(); // ターン開始処理
    }
}

