using UnityEngine;

public class ButtonPresenter
{
    ButtonView _buttonView;
    TablePresenter _tablePresenter;

    public ButtonPresenter(ButtonView buttonView, TablePresenter tablePresenter)
    {
        _buttonView = buttonView;
        _tablePresenter = tablePresenter;

        // 固定のボタンイベントはここで登録
    }

    public void SetTurnEndButtonEvent()
    {
        _buttonView.OnPressTurnEnd += _tablePresenter.OnTurnEndButtonPressed;
    }

    public void UnsetTurnEndButtonEvent()
    {
        _buttonView.OnPressTurnEnd -= _tablePresenter.OnTurnEndButtonPressed;
    }
}
