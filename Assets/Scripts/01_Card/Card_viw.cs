using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card_viw : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name = null;
    [SerializeField] TextMeshProUGUI _power = null;
    [SerializeField] Image _texture = null;
    [SerializeField] Sprite _textureBack = null;

    Card_mdl _model;
    AudioSource _audioSource;
    bool _isMoved = false;

    public event Action<Card_viw> OnCardClicked; // カードがクリックされたときのイベント

    public void OnPointerClick() // カードクリックされたときに呼ばれるメソッド
    {
        OnCardClicked?.Invoke(this); // イベントを発火
    }

    /// <summary>
    /// カードのモデルを取得
    /// </summary>
    /// <returns></returns>
    public Card_mdl GetModel() // クリックされたカードが自身の情報を取得できるようにするためのメソッド
    {
        return _model;
    }

    /// <summary>
    /// カードを表面で表示
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public bool ShowCardFront(Card_mdl card)
    {
        if (_name != null)
        {
            _name.text = card.GetName();
            if (_power != null)
            {
                _power.text = card.GetPower().ToString();
                if (_texture != null)
                {
                    _texture.sprite = card.GetTexture();
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
    public bool ShowCardBack(Card_mdl card)
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
    public bool ShowCardEle(Card_mdl card)
    {
        if (_name != null)
        {
            Destroy(_name.gameObject);
            if (_power != null)
            {
                Destroy(_power.gameObject);
                if (_texture != null)
                {
                    _texture.sprite = card.GetTexture();
                    return SetCardInfo(card, "Ele");
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Toggleでカードを上下に移動
    /// </summary>
    /// <param name="card"></param>
    public void MoveCard()
    {
        Vector3 pos = this.transform.localPosition;
        if (_isMoved)
        {
            this.transform.localPosition = new Vector3(pos.x, 0, pos.z);
            _isMoved = false;
        }
        else
        {
            this.transform.localPosition = new Vector3(pos.x, 10, pos.z);
            _isMoved = true;
        }  
    }

    /// <summary>
    /// カードを置くときの音を再生
    /// </summary>
    /// <param name="card"></param>
    public void PlaySound()
    {
        if (_audioSource == null)
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
        }
        _audioSource.Play();
    }

    // ----------内部メソッド----------

    bool SetCardInfo(Card_mdl card, string how) // 共通の動作
    {
        _model = card; // 引数で受け取ったカードモデルを保持
        if (how == "Front" || how == "Back" || how == "Ele")
        {
            this.name = $"Card_{_model.GetID()}_{how}"; // オブジェクト名を設定
            return true;
        }
        return false;
        
    } // stringによる分岐は悪手だけど，privateな閉じた動作のため許容とする
}
