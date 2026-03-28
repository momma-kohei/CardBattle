using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")] // 右クリックから作れるようにアセットメニューを作成
// Assets/Resources/CardDatas/下にこれを用いてカード集を作成

// unityのアセットとしてデータを管理
public class CardData : ScriptableObject // unity -> data
{
    // カードデータ
    public Sprite cardTexture; // 画像
    public string description; // 説明
}
