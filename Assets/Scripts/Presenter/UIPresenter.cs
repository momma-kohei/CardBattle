using UnityEngine;

public class UIPresenter
{
    PlayerModel _playerModel1;
    PlayerModel _playerModel2;
    CardPresenter _cardPresenter;
    PlayerUIView _playerUIView1;
    PlayerUIView _playerUIView2;
    UIView _uiView;

    public UIView UIView { get { return _uiView; } }

    public UIPresenter(PlayerModel playerModel1, PlayerModel playerModel2, CardPresenter cardPresenter, UIView uiView, PlayerUIView playerUIView1, PlayerUIView playerUIView2)
    {
        _playerModel1 = playerModel1;
        _playerModel2 = playerModel2;
        _cardPresenter = cardPresenter;
        _playerUIView1 = playerUIView1;
        _playerUIView2 = playerUIView2;
        _uiView = uiView;
        _uiView.OnClickNextButton += AttackerToDefender;
    }

    public void TurnEndEvent(PlayerModel _playerModel)
    {
        _uiView.OnClickTurnEndButton += _playerModel.TurnEnd;
        // _uiView.OnClickTurnEndButton += _cardPresenter.StartGame;

    }

    void AttackerToDefender()
    {
        Debug.Log("Defender Phase");
        _uiView.OnClickNextButton -= AttackerToDefender;
        _cardPresenter.DefenderPhase();
        _uiView.OnClickNextButton += DefenderToBattle;
    }

    void DefenderToBattle()
    {
        Debug.Log("Battle Phase");
        _uiView.OnClickNextButton -= DefenderToBattle;
        _cardPresenter.BattlePhase();
        _cardPresenter.PrintHP();
        _playerUIView1.ShowHP(_playerModel1);
        _playerUIView2.ShowHP(_playerModel2);
        _uiView.OnClickNextButton += BattleToNextTurn;
    }

    void BattleToNextTurn()
    {
        Debug.Log("Attacker Phase");
        _uiView.OnClickNextButton -= BattleToNextTurn;
        _cardPresenter.NextTurn();
        _cardPresenter.AttackerPhase();
        _uiView.OnClickNextButton += AttackerToDefender;
    }
}
