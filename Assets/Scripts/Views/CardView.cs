using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// データ情報をもとにカードを画面に表示するクラス
/// </summary>
public class CardView : MonoBehaviour // data -> display
{
    public event Action<CardView> OnClicked; // カードがクリックされたときのイベント

    // Modelから受け取ったデータを表示するためのUI要素
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI powerText = null;
    [SerializeField] Image cardTexture = null;
    [SerializeField] Sprite cardTextureBack = null;

    // やっぱりモデルを保持しておくことにする
    CardModel _cardModel; // このカードのモデル情報を保持する変数
    public CardModel CardModel => _cardModel; // カードモデルのプロパティ

    // カードを浮かせる処理に関係する変数
    //bool _isSelected = false; // このカードが選択されているかどうかを示すフラグ
    //public bool IsSelected => _isSelected; // 選択フラグのプロパティ

    public void OnPointerClick() // カードがクリックされたときに呼ばれるメソッド
    {
        //Debug.Log($"{this.name}がクリックされました。");
        OnClicked?.Invoke(this); // カードがクリックされたときにイベントを引き起こす


        //if (_isSelected)
        //{
        //    _isSelected = false;
        //    // 選択解除の視覚効果を追加する場合はここに記述
        //}
        //else
        //{
        //    _isSelected = true;
        //    // 選択の視覚効果を追加する場合はここに記述
        //}
    }

    ///// <summary>
    ///// カードモデルの情報をUIとして表示するメソッド
    ///// </summary>
    ///// <param name="cardModel"></param>
    //public void Show(CardModel _cardModel, CardListView _cardListView)
    //{
    //    if (nameText != null) nameText.text = _cardModel.cardName;
    //    if (powerText != null) powerText.text = _cardModel.power.ToString();
    //    if (cardTexture != null) cardTexture.sprite = _cardModel.cardTexture;
    //    this._cardModel = _cardModel; // 引数で受け取ったカードモデルを保持
    //    this._cardListView = _cardListView; // 引数で受け取ったListViewのインスタンスを保持
    //    this.name = $"Card_{_cardModel.cardID}"; // わかりやすいようにオブジェクト名を設定
    //}

    public void ShowCard(CardModel _cardModel, char cardState)
    {
        //if (nameText != null && cardState == 'F') nameText.text = _cardModel.cardName;
        //if (powerText != null && cardState == 'F') powerText.text = _cardModel.power.ToString();
        //if (cardTexture != null && (cardState == 'F' || cardState == 'E')) cardTexture.sprite = _cardModel.cardTexture;
        //this._cardModel = _cardModel; // 引数で受け取ったカードモデルを保持
        //this.name = $"Card_{_cardModel.cardID}_{cardState}"; // わかりやすいようにオブジェクト名を設定

        switch (cardState)
        {
            case 'F':
                if (nameText != null) nameText.text = _cardModel.cardName;
                if (powerText != null) powerText.text = _cardModel.power.ToString();
                if (cardTexture != null) cardTexture.sprite = _cardModel.cardTexture;
                break;
            case 'B':
                if (nameText != null) Destroy(nameText.gameObject);
                if (powerText != null) Destroy(powerText.gameObject);
                if (cardTexture != null) cardTexture.sprite = cardTextureBack;
                break;
            case 'E':
                if (nameText != null) Destroy(nameText.gameObject);
                if (powerText != null) Destroy(powerText.gameObject);
                if (cardTexture != null) cardTexture.sprite = _cardModel.cardTexture;
                break;
            default:
                Debug.LogError("Invalid card state. Use 'F', 'B', or 'E'.");
                break;
        }
        this._cardModel = _cardModel; // 引数で受け取ったカードモデルを保持
        this.name = $"Card_{_cardModel.cardID}_{cardState}"; // わかりやすいようにオブジェクト名を設定
    }
}

