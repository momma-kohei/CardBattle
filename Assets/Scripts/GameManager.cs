using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CardManager cardManager;

    void Start()
    {
        cardManager.GenerateHandCard(11);
        cardManager.GenerateHandCard(12);
        cardManager.GenerateHandCard(24);
        cardManager.GenerateHandCard(37);
    }
}
