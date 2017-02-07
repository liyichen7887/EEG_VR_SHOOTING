using UnityEngine;
using UnityEngine.UI;

public class TouchPoseTest : MonoBehaviour {

    
    public Text debugText;
    public OVRInput.Controller c;
    public float f;
    public bool b;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       // f = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        f = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

        b=  OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);
        debugText.text = " " + f;

    }
}
