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
    [SerializeField] PlayerUIView _playerUIView1;
    [SerializeField] PlayerUIView _playerUIView2;
    [SerializeField] UIView _uiView;
    UIPresenter _uiPresenter;

    void Start()
    {
        Debug.Log("GameManager Start");

        // 初期化
        // モデルのインスタンス化
        _deckModel = new DeckModel();
        _playerModel1 = new PlayerModel(_deckModel, "Player1", _playerIcon1);
        _playerModel2 = new PlayerModel(_deckModel, "Player2", _playerIcon2);
        // 先手後手の決定
        _playerModel1.DecideFirstAttacker(_playerModel1, _playerModel2);
        // プレゼンターのインスタンス化
        _cardPresenter = new CardPresenter(_playerModel1, _playerModel2, _playerCardView, _opponentCardView, _playerUIView1, _playerUIView2);
        _uiPresenter = new UIPresenter(_playerModel1, _playerModel2, _cardPresenter, _uiView, _playerUIView1, _playerUIView2);


        // ゲーム開始
        _cardPresenter.StartGame();
    }
}

