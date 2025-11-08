using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CardController cardPrefab; // プレハブを取得
    [SerializeField] Transform PlayerHandTransform; // 手札のTransformを取得

    void Start()
    {
        GenerateCard(PlayerHandTransform);
    }

    void GenerateCard(Transform hand)
    {
        CardController card = Instantiate(cardPrefab, hand, false); // 手札にカードを生成
        card.Init(11);
        for (int i = 21; i < 27; i++)
        {
            CardController cardi = Instantiate(cardPrefab, hand, false); // 手札にカードを生成
            cardi.Init(i);
        }
    }
}
