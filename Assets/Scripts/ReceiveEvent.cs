using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiveEvent : MonoBehaviour
{
    GameManager manager;
    private bool inField = false;

    /// <summary>
    /// ゲームマネージャーをセットするメソッド 
    /// </summary>
    /// <param name="gm"></param>
    public void SetGameManager(GameManager gm)
    {
        manager = gm;
    }

    public void MyPointerDownUI()
    {
        Debug.Log(this.name + "が押された");

        if(inField)
        {
            Destroy(GameObject.Find("/PlayCanvas/PlayerField/" + this.name)); // フィールド内に作成したオブジェクトを破壊
            inField = false;
        }
        else
        {
            manager.GenerateFieldCard(int.Parse(this.name)); // フィールド内に自身と同じカードを作成
            inField = true;
        }
    }

    public void MyDragUI()
    {

        transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
    }
}
