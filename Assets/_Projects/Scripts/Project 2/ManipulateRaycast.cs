using UnityEngine;
using UnityEngine.UI;

public class ManipulateRaycast : MonoBehaviour
{

    //raycast performed using position and forward vector of the following transform
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
    private Floor floor;
    private Vector3 hitPoint;
 //   private SelectableObjects sObj;

    private Transform initialTransform;
    private bool Manipulating = false;
    // Use this for initialization
    void Start()
    {
        
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
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (!pointing)
        {
            hitLocation = null;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
            return;
        }


        if (Physics.Raycast(ray, out hit, 20.0f))
        {
            hitLocation = hit.transform;
            hitPoint = hit.point;

            lr.SetPosition(0, RaycastObject.position);
            lr.SetPosition(1, hitPoint);
        }
        else
        {
            hitLocation = null;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }
    }

    private void PerformManipulation()
    {
        if (!Manipulating)
            return;
        if (!hitLocation)
            return;

        so.PivotTransform.position = new Vector3(hitPoint.x, so.PivotTransform.position.y, hitPoint.z);
        Vector3 c = OVRInput.GetLocalControllerRotation(rotationController).eulerAngles;
        Vector3 objR = so.PivotTransform.rotation.eulerAngles;
        Vector3 nEuler = new Vector3(objR.x, c.y, objR.z);
        Quaternion q = Quaternion.Euler(nEuler);
        so.PivotTransform.rotation = q;
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