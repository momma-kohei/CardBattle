using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenterButtonView : MonoBehaviour
{
    [SerializeField] Button _centerButton;
    [SerializeField] TextMeshProUGUI _centerButtonText;
    [SerializeField] Image _centerButonBGD;

    public Action OnCenterButtonClicked; // センターボタンがクリックされたときのイベント
    AudioSource _buttonSound;

    public void OnCenterButtonPressed()
    {
        OnCenterButtonClicked?.Invoke(); // センターボタンがクリックされたときのイベントを呼び出す
        SetEvent(false); // ボタンがクリックされたらイベントを解除
    }

    public void SetEvent(bool on)
    {
        OnCenterButtonClicked -= PlayButtonSound;
        if (on) OnCenterButtonClicked += PlayButtonSound;
        SetButtonText(on);
    }

    void PlayButtonSound()
    {
        if (_buttonSound == null)
        {
            _buttonSound = _centerButton.GetComponent<AudioSource>();
        }
        _buttonSound.Play();
    }

    void SetButtonText(bool on) // Buttonの見た目を変更するメソッド
    {
        _centerButton.interactable = on;
        _centerButtonText.text = on ? "END" : "";
    }
}
