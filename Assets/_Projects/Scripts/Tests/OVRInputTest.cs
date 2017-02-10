using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputTest : MonoBehaviour {
    public OVRInput.Button loadButton = OVRInput.Button.PrimaryThumbstick;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetUp(loadButton))
        {
            Debug.Log("Dpad right up");
        }
	}
}
