using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    // どのシーンからでもアクセスできるように「シングルトンパターン」を採用
    public static MyPlayer Instance { get; private set; } // シングルトンインスタンス
    
    public Sprite[] playerIcons;

    string playerName = ""; // プレイヤー名を格納する変数
    Sprite playerIcon;
    bool isOnline = false;
    int cpuLevel = 0;

    public string PlayerName { get => playerName; set => playerName = value; }
    public Sprite PlayerIcon { get => playerIcon; set => playerIcon = value; }
    public bool IsOnline { get => isOnline; set => isOnline = value; }
    public int CPULevel { get => cpuLevel; set => cpuLevel = value; }

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

    public void SetIcon()
    {
        // アイコン決定処理
        int iconIndex = ((PlayerName.GetHashCode() % 10) + 10) % 10; // 負の値を防ぐ
        if (iconIndex >= 0 && iconIndex < playerIcons.Length)
        {
            playerIcon = playerIcons[iconIndex];
        }
        else
        {
            throw new System.IndexOutOfRangeException("Icon index is out of range.");
        }
    }
}
