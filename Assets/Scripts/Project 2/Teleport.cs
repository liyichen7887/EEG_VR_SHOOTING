using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    

    public Transform ObjectToTP;
    public AudioClip teleportSound;
    [Header("KeySettings")]
    public OVRInput.Button touchKey = OVRInput.Button.One;
    public KeyCode keyboardKey = KeyCode.T;

    [Header("Debug Settings")]
    public Text raycastResult;

    private RaycastHit hit;
    private Ray ray;
    private Transform hitLocation;
    private Interactable focusedObject;
    private Vector3 hitPoint;
    private AudioSource audioSource;
    private bool canTP;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canTP = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 15.0f, Color.red);
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            hitLocation = hit.transform;
            focusedObject = hitLocation.GetComponent<Interactable>();
            hitPoint = hit.point;
            raycastResult.text = "Result: " + hitLocation.name;
            canTP = (focusedObject) ? true : false;
            

        }
        else
        {
            raycastResult.text = "Result: ---" ;
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