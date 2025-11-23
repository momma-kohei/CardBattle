using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// ゲームの進行を制御するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CardListView _cardListView;

    void Start()
    {
        _cardListView.CardPresenter.StartGame();
    }
}

