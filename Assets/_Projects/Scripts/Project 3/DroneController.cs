using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using UnityEngine.UI;

public class DroneController : MonoBehaviour {

    public Text t;
    public DroneState state = DroneState.Stop;
    public float flySpeed = 4.0f;
    
    [Header("Line Render")]
    public LineRenderer lr;
    public float lineLength = 100.0f;
    [Header("Hands")]
    public GameObject leftHand;
    public GameObject rightHand;

    [HideInInspector]
    public int nextTargetCheckPoint = 1;
    private CharacterController c_controller;
    private Vector3 flyTowards = Vector3.zero;
    private LeapProvider provider;

    private bool useIndexFingerAsDirection = false;

    private GameObject timerUI;
    private Timer timerScript;
	// Use this for initialization
	void Start () {
        timerUI = GameObject.Find("CountDown");
        timerScript = timerUI.GetComponent<Timer>();
        c_controller = GetComponent<CharacterController>();
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        if (!provider)
            Debug.LogError("Leap Provider not found");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(timerScript.timeElapsed)
            PerformDroneMovement();
	}

    public void PerformDroneMovement()
    {
        if (state == DroneState.Stop) return;

        if (useIndexFingerAsDirection)
        {
            Frame frame = provider.CurrentFrame;
            if (frame.Hands.Count == 0)
            {
                lr.SetPosition(0, Vector3.zero);
                lr.SetPosition(1, Vector3.zero);
            }

            foreach (Hand hand in frame.Hands)
            {
                if (hand.IsRight)
                {
                    //draws where the index finger is pointing at
                    Bone[] bones = hand.Fingers[1].bones;
                    Bone bone = bones[3];
                  //  Vector c = bone.Center;
                    Vector d = bone.Direction;
                    // Vector3 center = new Vector3(c.x, c.y, c.z);
                    Vector3 center = transform.position;
                    Vector3 direction = new Vector3(d.x, d.y, d.z);
                    Debug.DrawRay(center, direction * 100f, Color.black);
                    lr.SetPosition(0, center);
                    lr.SetPosition(1, center + direction * 500f);
                    flyTowards = direction;
                }
            }

        }
        else
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }


          c_controller.Move(flyTowards * flySpeed);
      //  Vector3 p = transform.position;
      //  p += flyTowards * flySpeed;
      //  transform.position = p;


    }



    public void MoveWhereLeftHandisPointingAt()
    {
       // Debug.Log("Left hand event called");
        state = DroneState.Forward;
    }

    public void MoveWhereRightHandisPointingAt()
    {
      //  Debug.Log("right hand event called");
        state = DroneState.Forward;
        useIndexFingerAsDirection = true;
    }

    public void StoppedUsingIndexFinger()
    {
        useIndexFingerAsDirection = false;
    }


    public void StopMoving()
    {
       // Debug.Log("Stop  called");
        state = DroneState.Stop;
    }

    public void FillerEvent1()
    {
      //  Debug.Log("Filler Event 1 Called");
        t.text = "Activated";
    }


    public void FillerEvent2()
    {
      //  Debug.Log("Filler Event 2 Called");
        t.text = "deactivate";
    }

    public void CheckPointReached()
    {
        nextTargetCheckPoint++;
        if(nextTargetCheckPoint > LoadCheckPoints.totalNumCheckPoint)
        {
            // game ending here
        }
    }


}


public enum DroneState
{
    Forward, Stop
}