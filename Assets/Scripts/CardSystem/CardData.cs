using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")] // 右クリックから作れるようにメニューに追加
// Assets/Resources/CardDatas/ にこれを用いてカードデータ集を作成．これは設定資料のような立ち位置
public class CardData : ScriptableObject // スクリプタブルオブジェクト；カードデータのテンプレート
{
    // カードに共通するパラメータを宣言
    public string cardName; // 名前
    public int power; // 強さ
    public ElementType element; // 属性（enum:選択式になる）
    public Sprite cardImage; // 画像
    public string description; // 説明
}

public enum ElementType
{
    Fire, // 火属性
    Water, // 水属性
    Grass // 草属性；ポケモンに倣って草をGrassとした
}