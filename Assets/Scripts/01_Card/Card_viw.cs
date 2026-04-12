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

    public Card_mdl Model { get { return _model; } set { _model = value; } }

    public event Action<Card_viw> OnCardClicked; // カードがクリックされたときのイベント

    public void OnPointerClick() // カードクリックされたときに呼ばれるメソッド
    {
        OnCardClicked?.Invoke(this); // イベントを発火
    }

    /// <summary>
    /// カードの情報を表示
    /// </summary>
    /// <param name="card">カードモデル</param>
    /// <param name="side">表示する面</param>
    /// <exception cref="ArgumentException"></exception>
    public void ShowCard(Card_mdl card, CardSide side)
    {
        _model = card;
        if(!IsVarid(card)) throw new ArgumentException("Invalid card data"); // 例外処理

        switch (side)
        {
            case CardSide.Back:
                Destroy(_name.gameObject);
                Destroy(_power.gameObject);
                _texture.sprite = _textureBack;
                break;

            case CardSide.Ele:
                Destroy(_name.gameObject);
                Destroy(_power.gameObject);
                _texture.sprite = card.Texture;
                break;

            case CardSide.Front:
                _name.text = card.Name;
                _power.text = card.Power.ToString();
                _texture.sprite = card.Texture;
                break;

            default:
                throw new ArgumentException("Invalid card side");
        }
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
    bool IsVarid(Card_mdl card) // 例外処理
    {
        return ((card != null) && (card.Name != null) && (card.Power != 0) && (card.Texture != null));
    }
}

/// <summary>
/// カードの面を定義する列挙型
/// </summary>
public enum CardSide
{
    Front,
    Back,
    Ele
}
