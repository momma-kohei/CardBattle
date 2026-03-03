using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    [SerializeField] GameObject levelSelectPanel; // CPUレベルを選択するパネル
    [SerializeField] GameObject _screen; // 画面全体を覆うオブジェクト（クリックできなくするため）


    public void OnSoloButtonClicked()
    {
        Debug.Log("Solo button pressed");
        levelSelectPanel.SetActive(true); // レベル選択パネルを表示
    }

    public void OnMultiButtonClicked()
    {
        Debug.Log("Multi button pressed");
    }

    public void OnLevelOneButtonClicked()
    {
        Debug.Log("Lv.1 button pressed");
        // ここでレベル1の選択に応じた処理を行う（例: レベル1のデータをロードするなど）
        if(_screen != null) _screen.SetActive(true); // 画面をクリックできなくする
        Invoke("SceneTransition", 0.2f); // 0.2秒後にシーン遷移
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button pressed");
        levelSelectPanel.SetActive(false); // レベル選択パネルを非表示
    }


    void SceneTransition() // BattleSceneをロードするメソッド
    {
        SceneManager.LoadScene("BattleScene");
    }
}
