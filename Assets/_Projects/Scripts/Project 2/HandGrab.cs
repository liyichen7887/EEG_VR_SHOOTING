using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandGrab : MonoBehaviour {

    public Text t;
    
    public float raycastLength = 2f;
    public float objMovementSpeed = 3.0f;
    public float advanceScale = 0.5f;
    public Transform objToTP;
    public Transform advanceReferenceTransform;
    public Transform objForRaycast;
    private bool grabbing = false;
    private SelectableObjects focusedObject;
    private SelectableObjects grabbedObject;
    [Header("Key Binding")]
    public OVRInput.Axis1D grabKey = OVRInput.Axis1D.SecondaryHandTrigger;
    public OVRInput.Button advanceKey = OVRInput.Button.Four;
    public OVRInput.Button startManipulateKey = OVRInput.Button.Two;

    public Vector3 controllerPositionLastFrame;
    public Vector3 controllerPositionCurrentFrame;
    private Vector3 initialObjPosition;
    private Vector3 initialControllerPosition;
    //used for raycasting
    private RaycastHit hit;
    private Transform hitTransform;
    private Vector3 hitPoint;
    private int frameCounter = 0;
    private float xdiff = 0f;
    private float zdiff = 0f;
    private bool startManipulating = false;
    // Use this for initialization

    private void Start()
    {
        controllerPositionLastFrame = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    // Update is called once per frame
    

    void Update () {
        CheckKeyInput();
        PerformRaycast();
       

        bool grabPressed = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0f;
        
        if ((!grabbing) && grabPressed) //if haven't grabbed, now grab
        {
            if (focusedObject)
            {
                grabbing = true;
                focusedObject.SetMaterialColor("_Color", focusedObject.selectionColor);
                initialObjPosition = focusedObject.transform.position;
                grabbedObject = focusedObject;
            }
        }
        else if(grabbing && !grabPressed) //let go of the object
        {
            grabbing = false;
            if (grabbedObject.selected)
            {
                grabbedObject.SetMaterialColor("_Color", grabbedObject.m_renderers[0].materials[0].GetColor("_Color"));
            }
            else
            {
                grabbedObject.SetMaterialColor("_Color", grabbedObject.defaultColor);
                grabbedObject = null;
            }
        }
        if(grabbing)
        {

            if (OVRInput.GetDown(startManipulateKey))
            {
                if (!startManipulating)
                {
                    initialObjPosition = grabbedObject.transform.position;
                    xdiff = 0;
                    zdiff = 0;
                }

                startManipulating = !startManipulating;
            }

            if (!startManipulating)
                return;
            controllerPositionCurrentFrame = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            xdiff += (controllerPositionCurrentFrame.x - controllerPositionLastFrame.x);
            zdiff += (controllerPositionCurrentFrame.z - controllerPositionLastFrame.z);
            if (grabbedObject.alignWithWall)
                xdiff = 0;

            grabbedObject.transform.position = initialObjPosition + new Vector3(-zdiff, 0, xdiff) * objMovementSpeed;



            controllerPositionLastFrame = controllerPositionCurrentFrame;
            Vector3 c = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch).eulerAngles;
            Vector3 objR = grabbedObject.transform.rotation.eulerAngles;
            Vector3 nEuler = new Vector3(objR.x, c.y, objR.z);
            Quaternion q = Quaternion.Euler(nEuler);
            grabbedObject.transform.rotation = q;
        }
       

    }

    public void CheckKeyInput()
    {
        if (OVRInput.GetUp(advanceKey))
        {
            Vector3 forward = advanceReferenceTransform.forward;
            forward.y = 0;
            objToTP.position = objToTP.position + forward* advanceScale;
        }
    }

    private void PerformRaycast()
    {
        Debug.DrawRay(objForRaycast.position, objForRaycast.right * raycastLength, Color.red );
        Ray ray = new Ray(objForRaycast.position, objForRaycast.right);
        if (Physics.Raycast(ray, out hit, raycastLength))
        {
            hitTransform = hit.transform;
            focusedObject = hitTransform.GetComponent<SelectableObjects>();
            hitPoint = hit.point;
        }
        else
        {
            focusedObject = null;
            hitTransform = null;
        }

    }


}
