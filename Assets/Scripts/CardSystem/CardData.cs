using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")] // 右クリックから作れるようにアセットメニューを作成
// Assets/Resources/CardDatas/下にこれを用いてカード集を作成

// unityのアセットとしてデータを管理
public class CardData : ScriptableObject // unity -> data
{
    // カードに共通する変数
    public string cardName; // 名前
    public int power; // カードのパワー（攻撃力）
    public EleType element; // 属性（enum:選択式になる）

    public Sprite cardTexture; // 画像
    public string description; // 説明
}
