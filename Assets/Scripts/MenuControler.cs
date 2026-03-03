using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class MenuControler : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _confirmMenu;
    [SerializeField] GameObject _screen;

    // ---------- サブUIに関するボタンイベント ----------
    public void OnMenuButtonPressed()
    {
        Debug.Log("Menu button pressed");
        AppearMenu(); // メニューを表示
    }
    public void OnExitButtonPressed()
    {
        Debug.Log("Exit button pressed");
        AppearExitConfirm(); // 確認ダイアログを表示
    }

    // ---------- 確認ダイアログに関するボタンイベント ----------
    public void OnExitYesButtonClicked()
    {
        _screen.SetActive(true); // 画面をクリックできなくする
        Invoke("SceneTransition", 0.2f); // 0.2秒後にシーン遷移
    }

    public void OnExitNoButtonClicked()
    {

        Debug.Log("No button pressed");
        DisappearExitConfirm(); // 確認ダイアログを非表示
    }

    // ---------- Menuに関するボタンイベント ---------- 

    public void OnCloseMenuButtonPressed()
    {
        Debug.Log("Close menu button pressed");
        DisappearMenu(); // メニューを非表示

    }

    public void OnSettingsButtonPressed()
    {
        Debug.Log("Settings button pressed");
    }

    public void OnHelpButtonPressed()
    {
        Debug.Log("Help button pressed");

    }

    // ---------- サブルーチン ----------

    void SceneTransition()
    {
        switch (SceneManager.GetActiveScene().name) // 現在のシーンに応じて遷移
        {
            case "TitleScene":
                break;
            case "SelectScene":
                SceneManager.LoadScene("TitleScene"); // タイトルシーンへ遷移
                break;
            case "BattleScene":
                SceneManager.LoadScene("SelectScene"); // 選択シーンへ遷移
                break;
            default:
                break;
        }
    }

    void AppearMenu()
    {
        if(_menu != null) _menu.SetActive(true); // メニューを表示
    }

    void DisappearMenu()
    {
        _menu.SetActive(false); // メニューを非表示
    }

    void AppearExitConfirm()
    {
        if(_confirmMenu != null) _confirmMenu.SetActive(true); // 確認ダイアログを表示
    }

    void DisappearExitConfirm()
    {
        _confirmMenu.SetActive(false); // 確認ダイアログを非表示
    }
}
