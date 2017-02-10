using UnityEngine;
using UnityEngine.UI;

public class ManipulateRaycast : MonoBehaviour
{

    //raycast performed using position and forward vector of the following transform
    public Text t;
    public Transform RaycastObject;
    public LineRenderer lr;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public SelectObject so;
    [Header("Key Bindings")]
    public KeyCode mainKey = KeyCode.Mouse0;
    public OVRInput.Button mainKeyTouch = OVRInput.Button.Two;
    public OVRInput.Button cancelKeyTouch = OVRInput.Button.Three;
    public OVRInput.Controller rotationController;

    private RaycastHit hit;
    private Transform hitLocation;
    public bool focusingFloor = false;
    public bool focusingWall = false;
    private Vector3 hitPoint;
 //   private SelectableObjects sObj;

    private Transform initialTransform;
    private bool Manipulating = false;
    private int wallLayerMask = 1 << 10;
    private int floorLayerMask = 1 << 11;
    private int combinedLayerMask;

    private void Start()
    {
        combinedLayerMask = wallLayerMask | floorLayerMask;

    }

    private void OnDisable()
    {
        if (lr)
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PerformRaycast();
        PerformManipulation();
        CheckKeyInput();
    }

    private void PerformRaycast()
    {
       

        bool pointing = !OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);
        if (!pointing)
        {
            hitLocation = null;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
            return;
        }
        else
        {
            hitLocation = null;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, RaycastObject.right*20.0f);
        }
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit, 20.0f, combinedLayerMask))
        {
            hitLocation = hit.transform;
            hitPoint = hit.point;

            lr.SetPosition(0, RaycastObject.position);
            lr.SetPosition(1, hitPoint);
            if(hitLocation.gameObject.tag == "wall")
            {
                focusingFloor = false;
                focusingWall = true;
                t.text = "Wall";
            }
            else if (hitLocation.gameObject.tag == "floor")
            {
                focusingFloor = false;
                focusingWall = true;
                t.text = "Floor";
            }
            else
            {
                focusingFloor = false;
                focusingWall = false;
                t.text = "None";
            }
        }
        else
        {
            hitLocation = null;
        }
    }

    private void PerformManipulation()
    {
        if (!Manipulating)
            return;
        if (!hitLocation)
            return;

        if (!so.pivotIsWhiteBoard)//handles case when pivot is not a whiteboard
        {
            so.PivotTransform.position = new Vector3(hitPoint.x, so.PivotTransform.position.y, hitPoint.z);
            Vector3 c = OVRInput.GetLocalControllerRotation(rotationController).eulerAngles;
            Vector3 objR = so.PivotTransform.rotation.eulerAngles;
            Vector3 nEuler = new Vector3(objR.x, c.y, objR.z);
            Quaternion q = Quaternion.Euler(nEuler);
            so.PivotTransform.rotation = q;
            drawNormal();
        }
        else//handles the case when pivot is a whiteboard
        {
            drawNormal();
        }

    }

    private void drawNormal()
    {

    }


    private void CheckKeyInput()
    {
        if (Input.GetKeyDown(mainKey) || OVRInput.GetDown(mainKeyTouch))
        {
            if (Manipulating)
            {
                Manipulating = false;
                so.PivotTransform.position = new Vector3(hitPoint.x, so.PivotTransform.position.y, hitPoint.z);
                audioSource.PlayOneShot(clickSound);
                so.SetCollidersActive(true);
            }
            else
            {
                if (!so.PivotTransform)
                    return;
                Manipulating = true;
                initialTransform = so.PivotTransform;
                audioSource.PlayOneShot(clickSound);
                so.SetCollidersActive(false);
            }
        }

    }


}