using cardLists;
using player;
using UnityEngine;

public class Hand1View : MonoBehaviour
{
    [SerializeField] CardView _cardPrefab; // カードプレハブの参照
    [SerializeField] Transform _transform; // 手札を表示するTransformの参照

    /// <summary>
    /// 自身の手札を表面で表示
    /// </summary>
    /// <param name="hand"></param>
    public void Show(Hand hand)
    {
        foreach (Transform _oldcard in _transform)
        {
            GameObject.Destroy(_oldcard.gameObject); // 表示エリアを初期化
        }
        for (int i = 0; i < hand.GetSize(); i++)
        {
            CardView card = Instantiate(_cardPrefab, _transform);
            card.ShowCardFront(hand.GetCard(i)); // 表面で表示
        }
    }

    /// <summary>
    /// Transformを取得
    /// </summary>
    /// <returns></returns>
    public Transform GetTransform()
    {
        return _transform;
    }

    // カードを浮かせる処理

    /// <summary>
    /// 選択して場札に出された手札のカードを浮かせる処理
    /// </summary>
    /// <param name="player">プレイヤーモデル</param>
    /// <exception cref="System.Exception"></exception>
    public void FloatSelectedHand(PlayerModel player)
    {
        if (player.GetArea().GetSize() > 0)
        {
            for (int i = 0; i < player.GetHand().GetSize(); i++) // 手札を探索
            {
                CardModel _handi = player.GetHand().GetCard(i);
                for (int j = 0; j < player.GetArea().GetSize(); j++) // 場札を探索
                {
                    CardModel _areaj = player.GetArea().GetCard(j);
                    if (_handi == _areaj)
                    {
                        FloatCard(_handi); // カードを浮かせる
                        break; // ある手札のカードが場札のカードと一致した時点でループを抜ける
                    }
                    else
                    {
                        SinkCard(_handi); // カードを戻す
                    }
                }
            }
        }
        else if (player.GetArea().GetSize() == 0) // 場札にカードが無い場合
        {
            for (int i = 0; i < player.GetHand().GetSize(); i++) // 手札を探索
            {
                CardModel _handi = player.GetHand().GetCard(i);
                SinkCard(_handi); // カードを戻す
            }
            Debug.Log("Area1のリストにカードがありません。");
        }
        else
        {
            throw new System.Exception("negative area size"); // 場札の枚数が負の場合
        }
    }

    void FloatCard(CardModel card) // 手札を浮かせる処理
    {
        Transform _ct = _transform.Find("Card_" + card.GetID().ToString() + "_Front");
        if (_ct != null)
        {
            _ct.localPosition = new Vector3(_ct.transform.localPosition.x, 10, _ct.transform.localPosition.z);
        }
        else Debug.Log("選択されたカードが子オブジェクトに見つかりませんでした。");
    }

    void SinkCard(CardModel card) // 手札を元の位置に戻す処理
    {
        Transform _ct = _transform.Find("Card_" + card.GetID().ToString() + "_Front");
        if (_ct != null)
        {
            _ct.localPosition = new Vector3(_ct.transform.localPosition.x, 0, _ct.transform.localPosition.z);
        }
        else Debug.Log("選択されたカードが子オブジェクトに見つかりませんでした。");
    }
}
