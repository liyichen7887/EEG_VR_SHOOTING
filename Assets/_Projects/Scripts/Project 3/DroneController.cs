using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using UnityEngine.UI;

public class DroneController : MonoBehaviour {
    public GameObject distance;
    public GameObject particle;
    public Transform particlePos;
    public Text t;
    public DroneState state = DroneState.Stop;
    public float flySpeed = 4.0f;
    public LoadCheckPoints lcp;
    [Header("Line Render")]
    public LineRenderer lr;
    public float lineLength = 100.0f;
    [Header("Hands")]
    public GameObject leftHand;
    public GameObject rightHand;

    public AudioClip hitSound;
    [HideInInspector]
    public int nextTargetCheckPoint = 8;  //1st checkpoint is where the player is at (1st set of coord in the competition file)
    private CharacterController c_controller;
    private Vector3 flyTowards = Vector3.zero;
    private LeapProvider provider;

    private bool useIndexFingerAsDirection = false;

    public GameObject timerUI;
    public GameObject stopwatch;


    private AudioSource audio;

    public static DroneController Instance;
    private void Awake()
    {
        Instance = this;
    }


	// Use this for initialization
	void Start () {
        particle.SetActive(false);
        c_controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        if (!provider)
            Debug.LogError("Leap Provider not found");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(timerUI.GetComponent<Timer>().timeElapsed)
            PerformDroneMovement();
	}

    public void PerformDroneMovement()
    {
        if (state == DroneState.Stop) return;

        if (useIndexFingerAsDirection)
        {
            Frame frame = provider.CurrentFrame;
            foreach (Hand hand in frame.Hands)
            {
                if (hand.IsRight)
                {
                    //draws where the index finger is pointing at
                    Bone[] bones = hand.Fingers[1].bones;
                    Bone bone = bones[3];
                    Vector d = bone.Direction;    
                    Vector3 center = transform.position;
                    Vector3 direction = new Vector3(d.x, d.y, d.z);
                    Debug.DrawRay(center, direction * 100f, Color.black);

                    flyTowards = direction;
                }
            }

        }

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + flyTowards * 500f);
        c_controller.Move(flyTowards * flySpeed);
      //  Vector3 currPostion = transform.position;
     //   transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currPostion-position), Time.fixedDeltaTime *3.0f);
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
            distance.GetComponent<Distance>().gameover = true;
            particle.transform.position = particlePos.position;
            particle.SetActive(true);
            stopwatch.GetComponent<Stopwatch>().setGameOver(true);
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string s = hit.transform.name;
        audio.PlayOneShot(hitSound);
        state = DroneState.Stop;
        MoveToLastCheckPoint();

    }


    private void MoveToLastCheckPoint()
    {
        transform.position = lcp.posOfCheckPointNumber(nextTargetCheckPoint - 1);
    }


}


public enum DroneState
{
    Forward, Stop
}