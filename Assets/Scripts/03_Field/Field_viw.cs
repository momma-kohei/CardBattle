using UnityEngine;
using ListModels;

public class Field_viw : MonoBehaviour
{
    [SerializeField] List_viw _handV1; // プレイヤー1の手札表示用View
    [SerializeField] List_viw _handV2; // プレイヤー2の手札表示用View
    [SerializeField] List_viw _areaV1; // プレイヤー1の場札表示用View
    [SerializeField] List_viw _areaV2; // プレイヤー2の場札表示用View

    public void ShowHand1(Hand_mdl handM)
    {
        _handV1.Show(handM);
    }

    public void ShowHand2(Hand_mdl handM)
    {
        _handV2.Show(handM);
    }

    public void ShowArea1(Area_mdl areaM)
    {
        _areaV1.Show(areaM);
    }

    public void ShowArea2(Area_mdl areaM)
    {
        _areaV2.Show(areaM);
    }
}
