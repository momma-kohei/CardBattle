using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name = null;
    [SerializeField] TextMeshProUGUI _power = null;
    [SerializeField] Image _texture = null;
    [SerializeField] Sprite _textureBack = null;

    CardInfoModel _cardModel;

    public event Action<CardInfoView> OnCardClicked; // カードがクリックされたときのイベント

    public void OnPointerClick() // カードクリックされたときに呼ばれるメソッド
    {
        OnCardClicked?.Invoke(this); // イベントを発火
    }

    /// <summary>
    /// カードのモデルを取得
    /// </summary>
    /// <returns></returns>
    public CardInfoModel GetModel()
    {
        return _cardModel;
    }

    /// <summary>
    /// カードを表面で表示
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public bool ShowCardFront(CardInfoModel card)
    {
        if (_name != null)
        {
            _name.text = _cardModel.GetName();
            if (_power != null)
            {
                _power.text = _cardModel.GetPower().ToString();
                if (_texture != null)
                {
                    _texture.sprite = _cardModel.GetTexture();
                    return SetCardInfo(card, "Front");
                }
            }
        }
        return false;
    }

    /// <summary>
    /// カードを裏面で表示
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public bool ShowCardBack(CardInfoModel card)
    {
        if (_name != null)
        {
            Destroy(_name.gameObject);
            if (_power != null)
            {
                Destroy( _power.gameObject);
                if (_texture != null)
                {
                    _texture.sprite = _textureBack;
                    return SetCardInfo(card, "Back");
                }
            }
        }
        return false;
    }

    /// <summary>
    /// カードを属性面で表示
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public bool ShowCardEle(CardInfoModel card)
    {
        if (_name != null)
        {
            Destroy(_name.gameObject);
            if (_power != null)
            {
                Destroy(_power.gameObject);
                if (_texture != null)
                {
                    _texture.sprite = _cardModel.GetTexture();
                    return SetCardInfo(card, "Ele");
                }
            }
        }
        return false;
    }

    // ----------内部メソッド----------

    bool SetCardInfo(CardInfoModel card, string how) // 共通の動作
    {
        _cardModel = card; // 引数で受け取ったカードモデルを保持
        if (how == "Front" || how == "Back" || how == "Ele")
        {
            this.name = $"Card_{_cardModel.GetID()}_{how}"; // オブジェクト名を設定
            return true;
        }
        return false;
        
    }
}
