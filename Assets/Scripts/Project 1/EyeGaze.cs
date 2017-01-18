using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RaycastSelector))]
[RequireComponent(typeof(Camera))]
public class EyeGaze : MonoBehaviour {

    // private Camera mainCam;

    public float TimeRequiredForAction = 3.0f;
    public Text statusText;
    private FireMode fireMode = FireMode.None;
    private float totalTimeGazed;

	// Use this for initialization
	void Start () {
        totalTimeGazed = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
        totalTimeGazed += Time.deltaTime;
	    if(totalTimeGazed >= TimeRequiredForAction)
        {
            performAction();
            totalTimeGazed = 0.0f;
        }
	}

    void performAction()
    {
        if(fireMode == FireMode.Laser)
        {
            ShootLaser();
        }
        else if(fireMode == FireMode.Cannon)
        {
            FireCannon();
        }

    }


    void ShootLaser()
    {



    }


    void FireCannon()
    {

    }



}

public enum FireMode
{
    None, Laser, Cannon
}
