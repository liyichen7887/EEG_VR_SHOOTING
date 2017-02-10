using UnityEngine;
using UnityEngine.UI;

public class ManipulateRaycast : MonoBehaviour
{

    public Transform RaycastObject;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public SelectObject so;
    public float wallOffset = 0.01f;
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

  //  private Transform initialTransform;
    private bool Manipulating = false;
    private int wallLayerMask = 1 << 10;
    private int floorLayerMask = 1 << 11;
    private int combinedLayerMask;
    //  private bool pointing    
    private void Start()
    {
        combinedLayerMask = wallLayerMask | floorLayerMask;

    }

    // Update is called once per frame
    void Update()
    {

        PerformRaycast();
        PerformManipulation();
        CheckKeyInput();

    }

   // public bool pointing;
    private void PerformRaycast()
    {

        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit, 100.0f, combinedLayerMask))
        {
            hitLocation = hit.transform;
            hitPoint = hit.point;

            if(hitLocation.gameObject.tag == "wall")
            {
                focusingFloor = false;
                focusingWall = true;
            }
            else if (hitLocation.gameObject.tag == "floor")
            {
                focusingFloor = true;
                focusingWall = false;
            }
            else
            {
                focusingFloor = false;
                focusingWall = false;
            }
        }
        else
        {
            hitLocation = null;
            focusingFloor = false;
            focusingWall = false;
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
        }
        else//handles the case when pivot is a whiteboard
        {
            if (!focusingWall)
                return;
            
            Vector3 position = new Vector3(hitPoint.x, so.PivotTransform.position.y, hitPoint.z); ;
            so.PivotTransform.position = position;
            so.PivotTransform.rotation = Quaternion.FromToRotation(Vector3.right, hit.normal);
        }

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
            //    initialTransform = so.PivotTransform;
                audioSource.PlayOneShot(clickSound);
                so.SetCollidersActive(false);
            }
        }

    }

}