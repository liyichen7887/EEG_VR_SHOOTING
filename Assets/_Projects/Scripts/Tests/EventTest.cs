using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour {



    public string m1 = "Event 1 has been triggered";
    public string m2 = "Event 2 has been triggered";


    public void Event1()
    {
        Debug.Log(m1);
    }


    public void Event2()
    {
        Debug.Log(m2);
    }
}
