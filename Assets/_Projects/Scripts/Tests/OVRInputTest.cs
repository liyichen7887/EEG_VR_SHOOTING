using UnityEngine.UI;
using UnityEngine;

public class OVRInputTest : MonoBehaviour {
    public OVRInput.Button loadButton = OVRInput.Button.PrimaryThumbstick;
    public Text t;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0f)
        {
           // Debug.Log("Dpad right up");
        }
        if (OVRInput.Get(OVRInput.Button.Two) )
        {
            Debug.Log("77777777777777");
        }


        //t.text = "value: " + OVRInput.Get(OVRInput.Touch.SecondaryThumbRest);
        t.text = "Acceleration : " + OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch);
    }
}
