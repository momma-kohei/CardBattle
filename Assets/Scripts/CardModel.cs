using UnityEngine;

/// <summary>
/// カードのデータを管理するクラス.
/// 各変数とそれを含むコンストラクタ．
/// </summary>
public class CardModel // 設定資料集CardDatasを参照して実際のカードを作るためのクラス
{
    // カードに共通する変数
    public string cardName; // 名前
    public int power; // カードのパワー（攻撃力）
    public ElementType element; // 属性（enum:選択式になる）
    public Sprite cardTexture; // 画像
    public string description; // 説明

    /// <summary>
    /// カードIDを指定してデータを取得し変数に格納するためのコンストラクタ
    /// </summary>
    /// <param name="cardID">属性番号と序列番号によるカードID</param>
    public CardModel(int cardID) // コンストラクタ（カードIDでデータを取得）
    {
        // cardIDからオブジェクトを特定し取得
        CardData cardData = Resources.Load<CardData>("CardDatas/Card" + cardID); // 場所とファイル名でロードするデータを指定
        cardName = cardData.cardName;
        power = cardData.power;
        element = cardData.element;
        cardTexture = cardData.cardImage;
        description = cardData.description;
    }
}
