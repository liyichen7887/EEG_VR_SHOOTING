using UnityEngine;
using UnityEngine.UI;

public class TouchPoseTest : MonoBehaviour {

    public OvrAvatar ava;
    public OvrAvatarLocalDriver avaDriver;
    public Text debugText;
    public OvrAvatarDriver.PoseFrame pose;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        avaDriver.GetCurrentPose( out pose);

    }
}
