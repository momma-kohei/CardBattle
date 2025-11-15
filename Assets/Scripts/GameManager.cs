using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CardManager CardManager;
    // [SerializeField] CardManager opponentCardManager;

    void Start()
    {
        CardManager.DrawPlayerHandRandom(7); // プレイヤーの初期手札を生成
        CardManager.DrawOpponentHandRandom(7); // 対戦相手の初期手札を生成
    }
}

