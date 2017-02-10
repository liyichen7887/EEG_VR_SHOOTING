
using UnityEngine;
using UnityEngine.UI;

public class Measurement : MonoBehaviour {


    public Text Result;
    public Transform RaycastObject;
    public OVRInput.Button button = OVRInput.Button.Two;
    private RaycastHit hit;
    private Transform hitTransform;
    private Vector3 hitPoint;
    public LineRenderer lr;
    // Use this for initialization
    private int index = 0;


    private void OnEnable()
    {
        if(Result)
         Result.enabled = true;

        if (lr)
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }
        Result.text = "Point 1 : 0 0 0\nPoint 2 : 0 0 0\nDistance: 0";
    }

    private void OnDisable()
    {
        if (lr)
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }

        if (Result)
            Result.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hitTransform = hit.transform;
            hitPoint = hit.point;
        }
        else
        {
            hitTransform = null;
            hitPoint = Vector3.zero;
        }

        if (OVRInput.GetDown(button))
        {
            if (!hitTransform) return;
            lr.SetPosition(index, hitPoint);
            index = (index + 1) % 2;  
        }
        Vector3 pos1 = lr.GetPosition(0);
        Vector3 pos2 = lr.GetPosition(1);
        Vector3 diff = pos1 - pos2;
        string s1 = "Point 1 : " + pos1.x + " " + pos1.y + " " + pos1.z + "\n";
        string s2 = "Point 2 : " + pos2.x + " " + pos2.y + " " + pos2.z + "\n";
        Result.text = s1 + s2 +"Distance: " + diff.magnitude;
    }
}
