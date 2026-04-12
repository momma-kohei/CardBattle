using TMPro;
using UnityEngine;

public class Result_viw : MonoBehaviour
{
    [SerializeField] GameObject _resultPanel;
    [SerializeField] TextMeshProUGUI _resultText;

    public void GameSet(int hp1, int hp2)
    {
        _resultPanel.SetActive(true); // 結果パネルを表示
        if (hp1 <= 0)
        {
            _resultText.text = "Lose...";
            _resultText.color = Color.cyan; // 負けたときはテキストを青色にする
        }
        else if (hp2 <= 0)
        {
            _resultText.text = "Win!!!";
            _resultText.color = Color.red; // 勝ったときはテキストを赤色にする
        }
    }
}
