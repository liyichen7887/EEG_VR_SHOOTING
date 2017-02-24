using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [HideInInspector]
    public int ID = -1;
    [HideInInspector]
    public LoadCheckPoints lcp;
    public bool cpReached;
    public TextMesh t;
	// Use this for initialization
	void Start ()
    {
        cpReached = false;
	}



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DroneController dc = other.GetComponent<DroneController>();
            if (dc.nextTargetCheckPoint == ID)
            {
                dc.CheckPointReached();
                cpReached = true;

            }
          
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

}
