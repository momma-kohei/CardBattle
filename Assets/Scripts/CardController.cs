using UnityEngine;

/// <summary>
/// カード自身の情報を扱うクラス
/// </summary>
public class CardController : MonoBehaviour
{
    CardView _view; // データを表示する機能を取得
    CardModel _model; // カードデータを取得するための器

    private void Awake()
    {
        _view = GetComponent<CardView>(); // CardViewのインスタンスを取得
    }

    /// <summary>
    /// カードIDを指定してカードを初期化するメソッド
    /// </summary>
    /// <param name="cardID">属性番号と序列番号によるカードID</param>
    public void Init(int cardID)
    {
        _model = new CardModel(cardID); // CardModelの情報を初期化
        _view.Show(_model); // 初期化した情報でカードUIを表示
        this.name = cardID.ToString(); // オブジェクト名をカードIDで更新
    }

    // カードモデルの情報を適宜知らせるメソッドが必要

    public int GetCardElement()
    {
        return (int)_model.element;
    }
}

