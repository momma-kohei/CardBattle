using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    // カードに共通するパラメータを宣言
    public string cardName; // カードの名前
    public int power; // カードの強さ
    public ElementType element; // enum:選択式になる
    public Sprite cardImage;
    public string description;
}

public enum ElementType
{
    Fire,
    Water,
    Grass // ポケモンに倣って草をGrassとした
}