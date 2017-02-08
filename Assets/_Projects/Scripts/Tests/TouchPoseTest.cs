using UnityEngine;
using UnityEngine.UI;

public class TouchPoseTest : MonoBehaviour {

    
    //public Text debugText;
    public OVRInput.Button c;
    public float f;
    public bool b;
    // Use this for initialization
    void Start () {
        Debug.Log("Test script attached to " + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {


        b=  OVRInput.GetUp(c);
        if (b)
        {
            Debug.Log("Key pressed");
        }


    }
}
