using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    //raycast performed using position and forward vector of the following transform
    public Transform RaycastObject;
    public Transform ObjectToTP;
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

    public bool canTP;
    // Use this for initialization
    void Start()
    {
        canTP = false;
    }

    private void OnDisable()
    {
       // lr.SetPosition(0, Vector3.zero);
       // lr.SetPosition(1, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
       

        Debug.DrawRay(RaycastObject.position, RaycastObject.up * 15.0f, Color.blue);
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit, 10.0f))
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

        if ((Input.GetKeyDown(keyboardKey) || OVRInput.GetUp(touchKey)) && canTP )
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