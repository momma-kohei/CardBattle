using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_mgr : MonoBehaviour
{
    [SerializeField] GameObject _levelSelectPanel; // CPUレベルを選択するパネル
    [SerializeField] GameObject _matchingWindow; // マッチングを開始するか確認するウインドウ
    [SerializeField] GameObject _screen; // 画面全体を覆うオブジェクト（クリックできなくするため）

    private void Start()
    {
        MyPlayer.Instance.IsOnline = false;
        MyPlayer.Instance.CPULevel = 0;
        PunManager.Instance.Disconnect();
    }

    public void OnSoloButtonClicked()
    {
        Debug.Log("Solo button pressed");
        _levelSelectPanel.SetActive(true); // レベル選択パネルを表示
    }

    public void OnMultiButtonClicked()
    {
        Debug.Log("Multi button pressed");
        MyPlayer.Instance.IsOnline = true; // オンラインフラグを立てる
        _matchingWindow.SetActive(true); // マッチング確認ウインドウを表示
    }


    public void OnLevelOneButtonClicked()
    {
        Debug.Log("Lv.1 button pressed");
        MyPlayer.Instance.CPULevel = 0; // !!!!!
        // ここでレベル1の選択に応じた処理を行う（例: レベル1のデータをロードするなど）
        if (_screen != null) _screen.SetActive(true); // 画面をクリックできなくする
        Invoke("SceneTransition", 0.2f); // 0.2秒後にシーン遷移
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button pressed");
        _levelSelectPanel.SetActive(false); // レベル選択パネルを非表示
    }


    void SceneTransition() // BattleSceneをロードするメソッド
    {
        SceneManager.LoadScene("BattleScene");
    }
    void MatchingSceneTransition() // BattleSceneをロードするメソッド
    {
        SceneManager.LoadScene("BattleScene");
    }

    // ---------- マッチング確認ダイアログに関するボタンイベント ----------
    public void OnMatchingYesButtonClicked()
    {
        MyPlayer.Instance.IsOnline = true;
        PunManager.Instance.Connect(); // マッチング開始
        //_screen.SetActive(true); // 画面をクリックできなくする
        //Invoke("MatchingSceneTransition", 0.2f); // 0.2秒後にシーン遷移
    }

    public void OnMatchingNoButtonClicked()
    {
        Debug.Log("No button pressed");
        _matchingWindow.SetActive(false); // 確認ダイアログを非表示
    }
}
