using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;


public class DroneController : MonoBehaviour {

    public DroneState state = DroneState.Stop;
    public float flySpeed = 4.0f;
    [Header("Hands")]
    public GameObject leftHand;
    public GameObject rightHand;

    private Vector3 direction = Vector3.zero;
    private LeapProvider provider;
    private CharacterController c_controller;

	// Use this for initialization
	void Start () {
        c_controller = GetComponent<CharacterController>();
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        if (!provider)
            Debug.LogError("Leap Provider not found");
    }
	
	// Update is called once per frame
	void Update () {
       // if (state == DroneState.Stop) return;

        if(state == DroneState.Forward)
        {
            c_controller.Move(direction * flySpeed);
        }
	}

    public void MoveWhereLeftHandisPointingAt()
    {
        Debug.Log("Left hand event called");
        state = DroneState.Forward;
        direction = leftHand.transform.forward;
    }

    public void MoveWhereRightHandisPointingAt()
    {
        Debug.Log("right hand event called");
        state = DroneState.Forward;
        direction = rightHand.transform.forward;

        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft) continue;

            List<Finger> fingers = hand.Fingers;
            foreach (Finger finger in fingers)
            {
                Bone[] bones = finger.bones;
                foreach (Bone bone in bones)
                {

                    Vector v = bone.Direction;

                }
            }
        }



    }


    public void StopMoving()
    {
        state = DroneState.Stop;
    }

    public void FillerEvent1()
    {
        Debug.Log("Filler Event 1 Called");
    }


    public void FillerEvent2()
    {
        Debug.Log("Filler Event 2 Called");
    }
}


public enum DroneState
{
    Forward, Stop
}