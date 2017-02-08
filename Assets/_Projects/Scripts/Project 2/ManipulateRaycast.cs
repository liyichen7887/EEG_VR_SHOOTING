using UnityEngine;
using UnityEngine.UI;

public class ManipulateRaycast : MonoBehaviour
{

    //raycast performed using position and forward vector of the following transform
    public Transform RaycastObject;
    public LineRenderer lr;
    public Text t;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public SelectObject so;
    [Header("Key Bindings")]
    public KeyCode mainKey = KeyCode.Mouse0;
    public OVRInput.Button mainKeyTouch = OVRInput.Button.Four;
    public KeyCode cancelKey = KeyCode.Mouse1;
    public OVRInput.Button cancelKeyTouch = OVRInput.Button.Three;

    private RaycastHit hit;
    private Transform hitLocation;
    private Floor floor;
    private Vector3 hitPoint;
 //   private SelectableObjects sObj;

    public Transform initialTransform;
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
        Debug.DrawRay(RaycastObject.position, RaycastObject.up * 15.0f, Color.green);
        Ray ray = new Ray(RaycastObject.position, RaycastObject.up);
        if (Physics.Raycast(ray, out hit, 20.0f))
        {
            hitLocation = hit.transform;
           // sObj = hitLocation.GetComponent<SelectableObjects>();
            hitPoint = hit.point;

            lr.SetPosition(0, RaycastObject.position);
            lr.SetPosition(1, hitPoint);
            t.text = "Result: " + hit.transform.name;

        }
        else
        {
            t.text = "Result: -- ";
            hitLocation = null;
           // sObj = null;
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

    }


    private void CheckKeyInput()
    {
        if (Input.GetKeyDown(mainKey) || OVRInput.Get(mainKeyTouch))
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
                Manipulating = true;
                initialTransform = so.PivotTransform;
                audioSource.PlayOneShot(clickSound);
                so.SetCollidersActive(false);
            }
        }
        /*
        if (Input.GetKeyDown(cancelKey) || OVRInput.Get(cancelKeyTouch))
        {
            if (Manipulating)
            {
                Manipulating = false;
                so.PivotTransform = initialTransform;
            }
        }*/
    }


}