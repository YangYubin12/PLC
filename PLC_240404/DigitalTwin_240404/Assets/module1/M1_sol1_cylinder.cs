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
        //키보드 화살표 위쪽을 누르면 실린더 전진
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            setTartget(2);
        }
        //키보드 화살표 위쪽을 누르면 실린더 후진
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            setTartget(0);
        }
    }
    public void setTartget(float value)
    {
        //입력한 value값만큼 실린더를 움직인다. 
        cylinder.SetDriveTarget(ArticulationDriveAxis.Z, value);
    }
}
