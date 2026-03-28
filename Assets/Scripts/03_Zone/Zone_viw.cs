using UnityEngine;

public class Zone_viw : MonoBehaviour
{
    [SerializeField] List_viw _handV; // 手札表示用View
    [SerializeField] List_viw _areaV; // 場札表示用View

    public void Show(Zone_mdl zoneM)
    {
        // ゾーンの表示処理
        _handV.Show(zoneM.GetHand());
        _areaV.Show(zoneM.GetArea());
    }
}
