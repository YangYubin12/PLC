using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_sol1_cylinder : MonoBehaviour
{
    private ArticulationBody cylinder;
    // Start is called before the first frame update
    void Start()
    {
        cylinder = GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ű���� ȭ��ǥ ������ ������ �Ǹ��� ����
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            setTartget(2);
        }
        //Ű���� ȭ��ǥ ������ ������ �Ǹ��� ����
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            setTartget(0);
        }
    }
    public void setTartget(float value)
    {
        //�Է��� value����ŭ �Ǹ����� �����δ�. 
        cylinder.SetDriveTarget(ArticulationDriveAxis.Z, value);
    }
}
