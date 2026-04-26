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

    // ルーム参加成功時
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room. Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    // 2人揃ったとき（ホスト側のみ実行）
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered. Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("BattleScene"); // 全員同時にシーン遷移
        }
    }

    // 切断時
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected: " + cause);
        if (_matchingWindow != null) _matchingWindow.SetActive(false);
    }
}