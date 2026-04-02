using ListModels;
using UnityEngine;

/// <summary>
/// 統括的なモデルクラスという立ち位置
/// リスト間のやりとりを行うためのメソッドを持つ
/// オンラインでメソッドを呼び出すために、オブジェクトにアタッチしする
/// </summary>
public class Field_ctl : MonoBehaviour
{
    Deck_mdl _deckM1;
    Hand_mdl _handM1;
    Area_mdl _areaM1;
    Hand_mdl _handM2;
    Area_mdl _areaM2;
    public Hand_mdl Hand1 { get { return _handM1; } }
    public Hand_mdl Hand2 { get { return _handM2; } }
    public Area_mdl Area1 { get { return _areaM1; } }
    public Area_mdl Area2 { get { return _areaM2; } }

    void Awake()
    {
        _deckM1 = new Deck_mdl();
        _handM1 = new Hand_mdl();
        _areaM1 = new Area_mdl();
        _handM2 = new Hand_mdl();
        _areaM2 = new Area_mdl();
    }

    /// <summary>
    /// 山札からカードを引く
    /// </summary>
    /// <returns>対象のカード</returns>
    public Card_mdl Pop()
    {
        return _deckM1.Pop();
    }

    /// <summary>
    /// 自分の手札にカードを追加する
    /// </summary>
    /// <param name="cardM">対象のカード</param>
    public void Draw1(Card_mdl cardM)
    {
        //if(_hand.GetSize() < _maxHandSize)
        _handM1.Add(cardM);
        _handM1.Sort();
    }

    /// <summary>
    /// 相手の手札にカードを追加する
    /// </summary>
    /// <param name="cardM">対象のカード</param>
    public void Draw2(Card_mdl cardM)
    {
        //if(_hand.GetSize() < _maxHandSize)
        _handM2.Add(cardM);
        _handM2.Sort();
    }

    /// <summary>
    /// 自分のカードを場札に追加/削除
    /// </summary>
    /// <param name="card">対象のカード</param>
    public void Toggle1(Card_mdl cardM)
    {
        if (!_areaM1.Add(cardM)) // 場札への追加に挑戦
        {
            _areaM1.Remove(cardM); // 失敗ならば場札からの削除に挑戦
        }
    }

    /// <summary>
    /// 相手のカードを場札に追加/削除
    /// </summary>
    /// <param name="card">対象のカード</param>
    public void Toggle2(Card_mdl cardM)
    {
        if (!_areaM2.Add(cardM)) // 場札への追加に挑戦
        {
            _areaM2.Remove(cardM); // 失敗ならば場札からの削除に挑戦
        }
    }

    /// <summary>
    /// 各場札のカードをすべて各手札から削除して、場札からも削除する
    /// </summary>
    public void Trash()
    {
        int loop = 0;
        Card_mdl cardM;
        while (_areaM1.GetSize() > 0)
        {
            cardM = _areaM1.GetCard(0); // 場札の最初のカードを取得
            _handM1.Remove(cardM); // 手札から削除
            _areaM1.Remove(cardM); // 場札から削除
            loop++;
            if (loop == 10) break; // 無限ループ回避用
        }
        loop = 0;
        while (_areaM2.GetSize() > 0)
        {
            cardM = _areaM2.GetCard(0); // 場札の最初のカードを取得
            _handM2.Remove(cardM); // 手札から削除
            _areaM2.Remove(cardM); // 場札から削除
            loop++;
            if (loop == 10) break; // 無限ループ回避用
        }
    }
}
