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
    private Ray ray;
    private Transform hitLocation;
    private Interactable focusedObject;
    private Vector3 hitPoint;

    private bool canTP;
    // Use this for initialization
    void Start()
    {
        canTP = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(RaycastObject.transform.position, RaycastObject.transform.forward * 15.0f, Color.red);
        Ray ray = new Ray(RaycastObject.transform.position, RaycastObject.transform.forward);
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            hitLocation = hit.transform;
            focusedObject = hitLocation.GetComponent<Interactable>();
            hitPoint = hit.point;
            raycastResult.text = "Raycast: " + hitLocation.name;
            canTP = (focusedObject) ? true : false;
            

        }
        else
        {
            raycastResult.text = "Raycast: ---" ;
            focusedObject = null;
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