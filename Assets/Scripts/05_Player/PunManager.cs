// Assets/Scripts/05_Player/PunManager.cs
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// PUN2の接続・マッチング管理クラス
/// </summary>
public class PunManager : MonoBehaviourPunCallbacks
{
    public static PunManager Instance { get; private set; }

    [SerializeField] GameObject _matchingWindow; // マッチング中の表示用

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Photonサーバーに接続
    /// </summary>
    public void Connect()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MyPlayer.Instance.PlayerName; // プレイヤー名を設定
        if (_matchingWindow != null) _matchingWindow.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    // 接続成功時
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomRoom(); // ランダムルームに参加を試みる
    }

    // ランダムルームが見つからなかった時
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating new room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    // Clientが部屋に入ったときに呼ばれる（自分自身が入ったとき）
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room. Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        // 既に2人いる部屋に入ったということはHostが待っていた = すぐ開始
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && !PhotonNetwork.IsMasterClient)
        {
            // Hostがシーン遷移するのを待つ（AutomaticallySyncSceneが機能する）
            Debug.Log("Joined as Client, waiting for host to load scene...");
        }
    }

    // 2人揃ったとき（ホスト側のみ実行）
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered. Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true; // 先に設定
            PhotonNetwork.LoadLevel("BattleScene");
        }
    }

    // 切断時
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected: " + cause);
        if (_matchingWindow != null) _matchingWindow.SetActive(false);
    }

    /// <summary>
    /// 自分がMasterClient（Host側）かどうか
    /// </summary>
    public bool IsMasterClient()
    {
        return PhotonNetwork.IsMasterClient;
    }

    /// <summary>
    /// 相手のプレイヤー名を取得
    /// </summary>
    public string GetEnemyName()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!player.IsLocal) return player.NickName;
        }
        return "Player2";
    }
}