using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1Lamp_control : MonoBehaviour
{
    //m1_s1, m1_cs1, m1_cs2
    public GameObject M1_S1;
    public GameObject M1_CS1;
    public GameObject M1_CS2;

    // Start is called before the first frame update
    Renderer lamp;
    void Start()
    {
        lamp = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //m1_s1 on
            M1_S1.GetComponent<lamp_control>().turnOn();
            //m1_cs1 off
            M1_CS1.GetComponent<lamp_control>().turnOff();
            //m1_cs2 off
            M1_CS2.GetComponent<lamp_control>().turnOn();

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            M1_S1.GetComponent<lamp_control>().turnOff();
            M1_CS1.GetComponent<lamp_control>().turnOn();
            M1_CS2.GetComponent<lamp_control>().turnOff();
        }

    }
}
