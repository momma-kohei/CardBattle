using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CardManager playerCardManager;
    // [SerializeField] CardManager opponentCardManager;

    void Start()
    {
        playerCardManager.InitDeck(); // デッキ情報を初期化
        // opponentCardManager.InitDeck(); // デッキ情報を共有
        playerCardManager.GenerateRandomHandCard(7); // ランダムに7枚手札を生成
        // opponentCardManager.GenerateRandomHandCard(7); // ランダムに7枚手札を生成
    }
}
