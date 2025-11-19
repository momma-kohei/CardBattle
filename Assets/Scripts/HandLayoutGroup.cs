using UnityEngine;

public class HandLayoutGroup : MonoBehaviour
{
    private float _width;
    private float _height;
    private int _childCount;
    void Start()
    {
        // 親オブジェクトの高さと幅を取得
        RectTransform _rect = GetComponent<RectTransform>();
        _width = _rect.sizeDelta.x;
        _height = _rect.sizeDelta.y;
        _childCount = 0;
    }

    void Update()
    {
        // 要素数が変更になったら並びを整える
        if (_childCount != this.transform.childCount)
        {
            SetHorizontalLayout();
        }
        _childCount = this.transform.childCount;
    }

    public void SetHorizontalLayout()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // 子オブジェクトを均等間隔に並べる
            Transform _child = this.transform.GetChild(i);
            float _left = _width * (i + 1) / (this.transform.childCount + 1) - _width / 2;
            _child.localPosition = new Vector3(_left, 0, 0); // 他属性選択中に実行されないよう修正が必要
        }
    }
}