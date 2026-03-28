using ListModels;

public class Zone_mdl
{
    Hand_mdl _hand;
    Area_mdl _area;
    int _maxHandSize = 7;

    public Zone_mdl()
    {
        _hand = new Hand_mdl();
        _area = new Area_mdl();
    }

    public Hand_mdl GetHand()
    {
        return _hand;
    }

    public Area_mdl GetArea()
    {
        return _area;
    }

    public bool DrawHand(Deck_mdl deck)
    {
        int loop = 0;
        CardModel card;
        while (_hand.GetSize() < _maxHandSize)
        {
            if (deck.GetSize() == 0) return false; // 山札が空ならば失敗
            else if (deck.GetSize() > 0)
            {
                card = deck.GetCard(0); // デッキの最初のカードを取得
                _hand.Add(card); // 手札にカードを追加
                deck.Remove(card); // デッキからカードを削除
                loop++;
                if (loop == 10) break; // 無限ループ回避用
            }
        }
        _hand.Sort(); // 手札をソート
        return true; // 成功
    }

    /// <summary>
    /// カードを場札に追加/削除
    /// </summary>
    /// <param name="card">対象のカード</param>
    public void Toggle(CardModel card)
    {
        if (!_area.Add(card)) // 場札への追加に挑戦
        {
            _area.Remove(card); // 失敗ならば場札からの削除に挑戦
        }
    }

    public Trash_mdl TrashArea()
    {
        Trash_mdl trash = new Trash_mdl();

        int loop = 0;
        CardModel card;
        while (_area.GetSize() > 0)
        {
            card = _area.GetCard(0); // 場札の最初のカードを取得
            trash.Add(card); // 場札のカードを捨て札に移動
            _hand.Remove(card); // 手札からカードを削除
            _area.Remove(card); // 場札からカードを削除
            loop++;
            if (loop == 10) break; // 無限ループ回避用
        }

        return trash;
    }
}
