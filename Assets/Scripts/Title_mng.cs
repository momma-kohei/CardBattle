using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameInputField; // プレイヤー名入力フィールド

    MyPlayer _myPlayer; // MyPlayerスクリプトの参照

    InputAction clickAction;
    AudioSource _audioSource;

    public void Start()
    {
        _myPlayer = MyPlayer.Instance;
        clickAction = InputSystem.actions.FindAction("Click"); // InputSystemから"Click"アクションを取得
        _audioSource = GetComponent<AudioSource>();
        if (_myPlayer != null || _myPlayer.PlayerName != "") // MyPlayerが存在しない、またはPlayerNameが空の場合
        {
            _nameInputField.text = _myPlayer.PlayerName; // 入力フィールドに入力済みのプレイヤー名を表示
        }
    }
    private void Update()
    {
        if (_nameInputField.text != "") // 入力フィールドが空でない場合
        {
            _myPlayer.PlayerName = _nameInputField.text; // MyPlayerのPlayerNameに入力値を設定

            if (clickAction.IsPressed()) // "Click"アクション発生
            {
                _audioSource.Play(); // クリック音を再生
                Invoke("LoadMainMenu", 1.5f); // 2秒後にMainMenuSceneへ遷移
            }
        }
    }

    void LoadMainMenu() // MainMenuSceneをロードするメソッド
    {
        SceneManager.LoadScene("SelectScene");
    }
}
