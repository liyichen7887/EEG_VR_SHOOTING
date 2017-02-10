using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRotationTest : MonoBehaviour
{

    public Transform RaycastObject;
    public Transform objToRotate;
    private RaycastHit hit;
    private Transform hitLocation;
    private Vector3 hitPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            hitLocation = hit.transform;
            hitPoint = hit.point;
          
            objToRotate.rotation = Quaternion.FromToRotation(Vector3.right, hit.normal);
        }
    }
}
