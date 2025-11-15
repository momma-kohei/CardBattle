using UnityEngine;

public class HandLayoutGroup : MonoBehaviour
{
    private float width;
    private float height;
    private int childCount;
    void Start()
    {
        // 親オブジェクトの高さと幅を取得
        RectTransform rect = GetComponent<RectTransform>();
        width = rect.sizeDelta.x;
        height = rect.sizeDelta.y;
        childCount = 0;
    }

    void Update()
    {
        // 要素数が変更になったら並びを整える
        if (childCount != this.transform.childCount)
        {
            SetHorizontalLayout();
        }
        childCount = this.transform.childCount;
    }

    public void SetHorizontalLayout()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // 子オブジェクトを均等間隔に並べる
            Transform child = this.transform.GetChild(i);
            float left = width * (i + 1) / (this.transform.childCount + 1) - width / 2;
            child.localPosition = new Vector3(left, 0, 0);
        }
    }
    public void SetVerticalLayout(int index)
    {
        Transform selectedChild = this.transform.GetChild(index);
        float left = width * (index + 1) / (this.transform.childCount + 1) - width / 2;
        selectedChild.localPosition = new Vector3(left, 10, 0);
    }
}