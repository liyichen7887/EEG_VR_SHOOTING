using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Color ReachedColor = Color.green;
    public LineRenderer lr;
    [HideInInspector]
    public DroneController dc;
    [HideInInspector]
    public int ID = -1;
    [HideInInspector]
    public LoadCheckPoints lcp;
    private bool cpReached;
    public TextMesh t;
    public AudioClip cpReachedSound;
    public AudioSource audS;
    private Material m;
	// Use this for initialization
	void Start ()
    {
        cpReached = false;
        dc = DroneController.Instance;
        m = GetComponent<MeshRenderer>().materials[0];
       // audS = GetComponent<AudioSource>();
       // lr = GetComponent<LineRenderer>();
	}



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audS.PlayOneShot(cpReachedSound);
           // Debug.Log("Entered checkpoint " + ID + "reached");
            if (dc.nextTargetCheckPoint == ID)
            {
                m.SetColor("_Color", ReachedColor);
                lr.material.SetColor("_Color", ReachedColor);
                dc.CheckPointReached();
                cpReached = true;

               // Debug.Log("Check Point " + ID + "reached");
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
