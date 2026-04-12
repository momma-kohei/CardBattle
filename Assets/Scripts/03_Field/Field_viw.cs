using UnityEngine;
using ListModels;

public class Field_viw : MonoBehaviour
{
    [SerializeField] List_viw _handV1; // プレイヤー1の手札表示用View
    [SerializeField] List_viw _handV2; // プレイヤー2の手札表示用View
    [SerializeField] List_viw _areaV1; // プレイヤー1の場札表示用View
    [SerializeField] List_viw _areaV2; // プレイヤー2の場札表示用View

    public Transform Hand1Transform { get { return _handV1.Place; } }
    public Transform Area1Transform { get { return _areaV1.Place; } }

    /// <summary>
    /// 手札を表示（自分の手札は表で、相手の手札は裏で表示）
    /// </summary>
    /// <param name="handM">表示する手札モデル</param>
    /// <param name="isMine">自分の手札かどうか</param>
    public void ShowHand(Hand_mdl handM, bool isMine)
    {
        if (isMine) _handV1.Show(handM, CardSide.Front);
        else        _handV2.Show(handM, CardSide.Ele);
    }

    /// <summary>
    /// 場札を表示（自分の場札は表で、相手の場札は裏で表示）
    /// </summary>
    /// <param name="areaM">表示する場札モデル</param>
    /// <param name="isMine">自分の場札かどうか</param>
    public void ShowArea(Area_mdl areaM, bool isMine)
    {
        if (isMine) _areaV1.Show(areaM, CardSide.Front);
        else        _areaV2.Show(areaM, CardSide.Back);
    }

    /// <summary>
    /// 場札を開示（プレイヤー2の場札を表で表示）
    /// </summary>
    /// <param name="areaM">表示する場札リストモデル</param>
    public void OpenArea(Area_mdl areaM)
    {
        _areaV2.Show(areaM, CardSide.Front);
    }
}
