using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _powerText = null;
    [SerializeField] TextMeshProUGUI _hpText = null;
    [SerializeField] Image _iconImage = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPlayerData(PlayerModel _playerModel)
    {
        _powerText.text = _playerModel.GetPower().ToString();
        _hpText.text = _playerModel.HitPoint.ToString();
        _iconImage.sprite = _playerModel.PlayerIcon.sprite;
    }
}
