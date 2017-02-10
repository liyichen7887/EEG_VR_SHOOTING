using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControllerSetup : MonoBehaviour {



    public OVRInput.Controller c;
	
	void Update () {
		transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
    }
}
