using UnityEngine;

public class Player_mdl
{
    string _name;
    Sprite _icon;

    public Player_mdl(string name, Sprite icon) // コンストラクタ
    {
        _name = name;
        _icon = icon;
    }

    // システム画面やUI用のゲッターとセッター
    #region Getter & Setter
    /// <summary>
    /// 名前を変更
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        _name = name;
    }

    /// <summary>
    /// 名前を取得
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return _name;
    }

    /// <summary>
    /// アイコンを変更
    /// </summary>
    /// <param name="icon"></param>
    public void SetIcon(Sprite icon)
    {
        _icon = icon;
    }

    /// <summary>
    /// アイコンを取得
    /// </summary>
    /// <returns></returns>
    public Sprite GetIcon()
    {
        return _icon;
    }
    #endregion
}
