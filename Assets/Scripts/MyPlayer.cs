using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    // どのシーンからでもアクセスできるように「シングルトンパターン」を採用
    public static MyPlayer Instance { get; private set; } // シングルトンインスタンス

    string playerName = ""; // プレイヤー名を格納する変数

    public string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }

    Sprite playerIcon;
    public Sprite PlayerIcon
    {
        get => playerIcon;
        set => playerIcon = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) // すでにインスタンスが存在する場合
        {
            Destroy(gameObject); // 重複するインスタンスを破棄
            return; // Awakeから抜ける
        }

        Instance = this; // そうでない場合、インスタンスを設定

        DontDestroyOnLoad(gameObject); // シーンが変わっても破棄されないようにする
    }
}
