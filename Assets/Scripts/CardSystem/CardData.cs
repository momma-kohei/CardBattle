using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")] // 右クリックから作れるようにアセットメニューを作成
// Assets/Resources/CardDatas/下にこれを用いてカード集を作成

// unityのアセットとしてデータを管理
public class CardData : ScriptableObject // unity -> data
{
    // カードに共通する変数
    public string cardName; // 名前
    public int power; // カードのパワー（攻撃力）
    public ElementType element; // 属性（enum:選択式になる）
    public Sprite cardImage; // 画像
    public string description; // 説明
}

/// <summary>
/// 属性の種類を定義する列挙型
/// </summary>
public enum ElementType
{
    Fire, // 火属性
    Water, // 水属性
    Grass // 草属性；ポケモンに倣って草をGrassとした
}
