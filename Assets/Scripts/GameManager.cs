using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CardManager playerCardManager;
    [SerializeField] CardManager02 CardManager;
    // [SerializeField] CardManager opponentCardManager;

    void Start()
    {
        // playerCardManager.InitDeck(); // デッキ情報を初期化
        // playerCardManager.GenerateRandomHandCard(7); // ランダムに7枚手札を生成
        CardManager.DrawPlayerHandRandom(7); // ランダムに7枚手札を生成
        CardManager.DrawOpponentHandRandom(7); // ランダムに7枚手札を生成
    }
}

