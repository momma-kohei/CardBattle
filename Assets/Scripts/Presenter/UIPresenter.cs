using UnityEngine;

public class UIPresenter
{
    PlayerModel _player1Model;
    PlayerModel _player2Model;

    string _player1Name = "Player 1"; // プレイヤー1の名前
    string _player2Name = "Player 2"; // プレイヤー2の名前


    public UIPresenter()
    {
        this._player1Model = new PlayerModel(_player1Name, null);
        this._player2Model = new PlayerModel(_player2Name, null);
    }
}
