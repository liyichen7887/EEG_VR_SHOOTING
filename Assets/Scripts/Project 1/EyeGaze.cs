using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class EyeGaze : MonoBehaviour {


    public float laserSpeed = 100.0f;
    public float cannonballSpeed = 200.0f;
    public float TimeRequiredForAction = 3.0f;
    public Text statusText;

    public Text raycastResult;

    public Image widget;
    public Image laserIcon;
    public Image cannonIcon;

    public FireMode fireMode = FireMode.None;
    public GameObject laserPrefab;
    public AudioClip laserSound;
    public GameObject cannonBallPrefab;
    public Color defaultColor = Color.white;
    private AudioSource audioS;
    private RaycastHit hit;
    private Ray ray;
    private float totalTimeGazed;
    private Interactable previousFocus; //gameObject from result of raycast from last frame;
    private Interactable focusedObject; //currently focus object
    private Transform camT;
    private List<GameObject> cannonballs;
    // Use this for initialization
    void Start() {
        totalTimeGazed = 0.0f;
        cannonIcon.color = defaultColor;
        laserIcon.color = defaultColor;

         camT = Camera.main.transform;
        cannonballs = new List<GameObject>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        PerformRaycast();
        CheckFocus();
        previousFocus = focusedObject;
    }


    void PerformRaycast()
    {
        Debug.DrawRay(camT.position, camT.forward*15, Color.red);
        Ray ray = new Ray(camT.position, camT.forward);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            focusedObject = objectHit.GetComponent<Interactable>();
            string objectName = objectHit.name;
            raycastResult.text = "Raycast Result: " + objectName;
        }
        else
        {
            raycastResult.text = "-------- ";
            focusedObject = null;
        }
    }

    void CheckFocus()
    {

        if (!previousFocus && focusedObject)
        {
            totalTimeGazed += Time.deltaTime;
        }
        else if (!focusedObject)
        {
            totalTimeGazed = 0f;
           
        }
        else if (previousFocus != focusedObject)
        {
            totalTimeGazed = 0.0f;   //reset timer if focus changed
        }
        else
        {
            totalTimeGazed += Time.deltaTime;
        }
       float pctComplete = totalTimeGazed / TimeRequiredForAction;
        widget.fillAmount = pctComplete;
       

        if (totalTimeGazed >= TimeRequiredForAction)
        {
            performAction();
            totalTimeGazed = 0.0f;
        }

    }

    

    void performAction()
    {
        
        if (focusedObject.GetType() == typeof(SelectFireMode))
        {
            fireMode = (focusedObject as SelectFireMode).fireMode;
            if(fireMode == FireMode.None)
            {
                cannonIcon.color = defaultColor;
                laserIcon.color = defaultColor;
            }
            else if(fireMode == FireMode.Cannon)
            {
                cannonIcon.color = Color.white;
                laserIcon.color = defaultColor;
            }
            else
            {
                cannonIcon.color = defaultColor;
                laserIcon.color = Color.white;
            }
            return;
        }
        if(fireMode == FireMode.Laser)
        {
            ShootLaser();
        }
        else if(fireMode == FireMode.Cannon)
        {
            FireCannonBall();
        }
        
    }


    void ShootLaser()
    {

        //   GameObject laserGO = Instantiate(laserPrefab) as GameObject;
        //    laserGO.transform.position = camT.position;
        //   laserGO.GetComponent<Rigidbody>().AddForce(camT.forward*laserSpeed);
        audioS.PlayOneShot(laserSound);
        (focusedObject as Brick).RespondToLaserAttack();
    }


    void FireCannonBall()
    {
        GameObject cannonballGO = Instantiate(cannonBallPrefab) as GameObject;
        cannonballs.Add(cannonballGO);
        cannonballGO.transform.position = camT.position;
        cannonballGO.GetComponent<Rigidbody>().AddForce(camT.forward*cannonballSpeed);

    }


    public void ResetCannonBalls()
    {
        foreach (GameObject g in cannonballs)
        {
            GameObject.Destroy(g);
        }
        cannonballs.Clear();
    }


}

public enum FireMode
{
    None, Laser, Cannon, Teleport
}
