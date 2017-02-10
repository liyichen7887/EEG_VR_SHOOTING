using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    //raycast performed using position and forward vector of the following transform
    public Transform RaycastObject;
    public Transform ObjectToTP;
    public SelectObject so;
    public AudioSource audioSource;
    public AudioClip teleportSound;

    [Header("Key Bindings")]
    public OVRInput.Button touchKey = OVRInput.Button.One;
    public KeyCode keyboardKey = KeyCode.T;

    [Header("Debug Settings")]
    public Text raycastResult;

    private RaycastHit hit;
    private Transform hitLocation;
    private Floor floor;
    private Vector3 hitPoint;
    private int floorLayerMask = 11 << 8;
    public bool canTP;
    private bool pointing;
    // Use this for initialization
    void Start()
    {
        canTP = false;
    }

    // Update is called once per frame
    void Update()
    {
        pointing = !OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);

        Debug.DrawRay(RaycastObject.position, RaycastObject.up * 15.0f, Color.blue);
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit, 40.0f, floorLayerMask ))
        {
            hitLocation = hit.transform;
            floor = hitLocation.GetComponent<Floor>();
            hitPoint = hit.point;
           // raycastResult.text = "Raycast: " + hitLocation.name;
            canTP = (floor) ? true : false;
        }
        else
        {
          //  raycastResult.text = "Raycast: ---" ;
            floor = null;
            canTP = false;
        }

        if (OVRInput.GetUp(touchKey) && canTP )
        {
            TP();
        }

    }



    private void TP()
    {
        if (!pointing)
            return;
        ObjectToTP.position = new Vector3(hitPoint.x, ObjectToTP.position.y, hitPoint.z);
        audioSource.PlayOneShot(teleportSound);
    }
}