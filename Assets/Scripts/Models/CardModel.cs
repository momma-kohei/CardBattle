using UnityEngine;

/// <summary>
/// カードのデータを管理するモデルクラス
/// カードデータから実際のカードを作成する
/// </summary>
public class CardModel // 設定資料集CardDatasを参照して実際のカードを作るためのクラス
{
    // カードに共通する変数
    public int cardID; // カードID（属性番号と序列番号による）
    public string cardName; // 名前
    public int power; // カードのパワー（攻撃力）
    public ElementType element; // 属性（enum:選択式になる）
    public Sprite cardTexture; // 画像
    public string description; // 説明
    public bool isSelected = false; // 選択されているかどうかのフラグ

    public CardModel(int cardID) // コンストラクタ
    {
        // cardIDからオブジェクトを特定し取得
        CardData cardData = Resources.Load<CardData>("CardDatas/Card" + cardID); // 場所とファイル名でロードするデータを指定
        this.cardID = cardID;
        cardName = cardData.cardName;
        power = cardData.power;
        element = cardData.element;
        cardTexture = cardData.cardTexture;
        description = cardData.description; // カードの説明は未実装，カードに効果を持たせる場合は使うかも
    }
}

