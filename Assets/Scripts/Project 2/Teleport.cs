using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    //raycast performed using position and forward vector of the following transform
    public Transform RaycastObject;
    public Transform ObjectToTP;
    public LineRenderer lr;

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

    private bool canTP;
    // Use this for initialization
    void Start()
    {
        canTP = false;
    }

    private void OnDisable()
    {
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
       

        Debug.DrawRay(RaycastObject.position, RaycastObject.forward * 15.0f, Color.red);
        Ray ray = new Ray(RaycastObject.position, RaycastObject.forward);
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            hitLocation = hit.transform;
            floor = hitLocation.GetComponent<Floor>();
            hitPoint = hit.point;
            raycastResult.text = "Raycast: " + hitLocation.name;
            canTP = (floor) ? true : false;

            lr.SetPosition(0, RaycastObject.position);
            lr.SetPosition(1, hitPoint);

        }
        else
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
            raycastResult.text = "Raycast: ---" ;
            floor = null;
            canTP = false;
        }

        if ((Input.GetKeyDown(keyboardKey) || OVRInput.Get(touchKey)) && canTP )
        {
            TP();
        }

    }



    private void TP()
    {
        ObjectToTP.position = new Vector3(hitPoint.x, ObjectToTP.position.y, hitPoint.z);
        audioSource.PlayOneShot(teleportSound);
    }
}