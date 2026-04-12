using UnityEngine;

public class CPU_mgr : MonoBehaviour
{
    [SerializeField] Field_mgr _field_Mgr;

    int _cpuLevel = 0; // GameManager的なシングルトンで受け渡したい

    public void Awake()
    {
        _cpuLevel = MyPlayer.Instance.CPULevel;
        _field_Mgr.CPUaction += SetCPU;
    }

    public void SetCPU()
    {
        switch (_cpuLevel)
        {
            case 0:
                CPU0();
                break;
        }
    }

    void CPU0()
    {
        _field_Mgr.Toggle(_field_Mgr.FieldM.Hand2.GetCard(0), false);
    }
}
