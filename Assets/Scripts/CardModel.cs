using UnityEngine;

public class CardModel
{
    // カードに共通するパラメータを取得する変数
    public string cardName; // 名前
    public int power; // 強さ
    public ElementType element; // 属性，enum:選択式になる
    public Sprite cardTexture; // 画像
    public string description; // 説明

    // コンストラクタ（カードIDでデータを取得）
    public CardModel(int cardID)
    {
        // cardIDからオブジェクトを特定し取得
        CardData cardData = Resources.Load<CardData>("CardDatas/Card" + cardID);
        cardName = cardData.cardName;
        power = cardData.power;
        element = cardData.element;
        cardTexture = cardData.cardImage;
        description = cardData.description;
    }
}
