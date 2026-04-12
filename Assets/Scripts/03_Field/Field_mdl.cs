using ListModels;

public class Field_mdl
{
    Deck_mdl _deckM1;
    Deck_mdl _deckM2;
    Hand_mdl _handM1;
    Hand_mdl _handM2;
    Area_mdl _areaM1;
    Area_mdl _areaM2;
    public Hand_mdl Hand1 { get { return _handM1; } }
    public Hand_mdl Hand2 { get { return _handM2; } }
    public Area_mdl Area1 { get { return _areaM1; } }
    public Area_mdl Area2 { get { return _areaM2; } }

    public Field_mdl()
    {
        _deckM1 = new Deck_mdl();
        _deckM2 = new Deck_mdl();
        _handM1 = new Hand_mdl();
        _areaM1 = new Area_mdl();
        _handM2 = new Hand_mdl();
        _areaM2 = new Area_mdl();
    }

    /// <summary>
    /// 山札からカードを引く
    /// </summary>
    /// <returns>対象のカード</returns>
    /// <param name="isMine">自分側の操作かどうか</param>
    public Card_mdl Pop(bool isMine)
    {
        return isMine ? _deckM1.Pop() : _deckM2.Pop();
    }

    /// <summary>
    /// 手札にカードを追加
    /// </summary>
    /// <param name="card">対象のカードモデル</param>
    /// <param name="isMine">自分側の操作かどうか</param>
    public void Draw(Card_mdl card, bool isMine)
    {
        Hand_mdl hand = isMine ? _handM1 : _handM2;
        hand.Add(card);
        hand.Sort();
    }

    /// <summary>
    /// カードを場札に追加/削除
    /// </summary>
    /// <param name="cardM">対象のカードモデル</param>
    /// <param name="isMine">自分側の操作かどうか</param>
    public void Toggle(Card_mdl cardM, bool isMine)
    {
        Area_mdl area = isMine ? _areaM1 : _areaM2;
        if (!area.Add(cardM)) // 場札への追加に挑戦
        {
            area.Remove(cardM); // 失敗ならば場札からの削除に挑戦
        }
    }

    /// <summary>
    /// 各場札のカードをすべて各手札から削除して、場札からも削除する
    /// </summary>
    public void Trash()
    {
        int loop = 0;
        Card_mdl cardM;
        while (_areaM1.Count > 0)
        {
            cardM = _areaM1.GetCard(0); // 場札の最初のカードを取得
            _handM1.Remove(cardM); // 手札から削除
            _areaM1.Remove(cardM); // 場札から削除
            loop++;
            if (loop == _handM2.Count) break; // 無限ループ回避用
        }
        loop = 0;
        while (_areaM2.Count > 0)
        {
            cardM = _areaM2.GetCard(0); // 場札の最初のカードを取得
            _handM2.Remove(cardM); // 手札から削除
            _areaM2.Remove(cardM); // 場札から削除
            loop++;
            if (loop == _handM1.Count) break; // 無限ループ回避用
        }
    }
}
