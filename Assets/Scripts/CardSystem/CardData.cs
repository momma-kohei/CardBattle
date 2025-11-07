using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    // カードに共通するパラメータを宣言
    public string cardName; // 名前
    public int power; // 強さ
    public ElementType element; // 属性，enum:選択式になる
    public Sprite cardImage; // 画像
    public string description; // 説明
}

public enum ElementType
{
    Fire,
    Water,
    Grass // ポケモンに倣って草をGrassとした
}