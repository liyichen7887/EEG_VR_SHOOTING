using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTransformTest : MonoBehaviour {

    public OVRInput.Controller controller;
    public Text t1;

    public GameObject obj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = OVRInput.GetLocalControllerPosition(controller);
        Quaternion rotation = OVRInput.GetLocalControllerRotation(controller) ;
        /*
        Quaternion objR = obj.transform.rotation; 
        Quaternion n = new Quaternion(objR.x, objR.y, rotation.z, objR.w);
       

        obj.transform.rotation = n;
        */
        Vector3 c = OVRInput.GetLocalControllerRotation(controller).eulerAngles;
        Vector3 objR = obj.transform.rotation.eulerAngles;
        Vector3 nEuler = new Vector3(objR.x, c.y, objR.z);
        Quaternion q = Quaternion.Euler(nEuler);
        obj.transform.rotation = q;
    }
}
