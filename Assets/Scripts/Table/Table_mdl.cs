

using ListModels;

public class Table_mdl
{
    Deck_mdl _deckM;
    Trash_mdl _trashM;
    Zone_mdl _zoneM1;
    Zone_mdl _zoneM2;

    public Table_mdl()
    {
        _deckM = new Deck_mdl();
        _trashM = new Trash_mdl();
        _zoneM1 = new Zone_mdl();
        _zoneM2 = new Zone_mdl();
    }

    public void FillHand1(Zone_mdl zoneM)
    {
        _zoneM1.DrawHand(_deckM);
    }

    public void FillHand2(Zone_mdl zoneM)
    {
        _zoneM2.DrawHand(_deckM);
    }
}
