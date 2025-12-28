using UnityEngine;

public class UIPresenter
{
    PlayerModel _playerModel1;
    PlayerModel _playerModel2;
    CardPresenter _cardPresenter;
    [SerializeField] PlayerUIView _playerUIView;
    [SerializeField] UIView _uiView;

    public UIPresenter(PlayerModel playerModel1, PlayerModel playerModel2, CardPresenter cardPresenter)
    {
        _playerModel1 = playerModel1;
        _playerModel2 = playerModel2;
        _cardPresenter = cardPresenter;
    }

    public void TurnEndEvent(PlayerModel _playerModel)
    {
        _uiView.OnClickTurnEndButton += _playerModel.TurnEnd;
        _uiView.OnClickTurnEndButton += _cardPresenter.StartGame;

    }
}
