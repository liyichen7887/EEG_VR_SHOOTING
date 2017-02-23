using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [HideInInspector]
    public int ID = -1;
    public LoadCheckPoints lcp;
	// Use this for initialization
	void Start () {
		
	}



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //other
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

}
