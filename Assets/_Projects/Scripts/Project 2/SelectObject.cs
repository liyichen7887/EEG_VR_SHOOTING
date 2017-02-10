using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour {
    public static SelectObject Instance;

    public SelectionMode activeSelectionMode = SelectionMode.Selection;
    public Teleport tp;
    public HandGrab hg;
    public ManipulateRaycast mr;
    public Transform RaycastObject;
    public LineRenderer lr;
    public RightThumStickControl rts;
    [Header("Audio Fields")]
    public AudioSource audioS;
    public AudioClip selectSound;
    public AudioClip letGoSound;


    [Header("UI Fields")]
   // public Text debug;
    public Text T_selectionMode;
    public Text T_teleportMode;
    public Text T_raycastMode;
    public Text T_freeformMode;
    public Color S_DefaultColor = Color.black;
    public Color S_SelectedColor = Color. green;

    [Header("Key Bindings")]
    public OVRInput.Button selectKeyTouch = OVRInput.Button.Two;

    
    //used for handling selection
    private List<SelectableObjects> selectedObjects;
  

    [Header("Misc")]
    public Transform RootTransform;
    public Transform root; //copy rootTramsform again, used for load/save reset
    /* when 1st item is selected, its transform will be set to PivotTransform,
     * and have RootTransform as its first child, subsequent selection will have
     * RootTransform as their parent, if the 1st item is getting deselected,
     *  
     */
    [HideInInspector]
    public Transform PivotTransform;

    //used for raycasting
    private RaycastHit hit;
    private Transform hitTransform;
     private Vector3 hitPoint;
    [HideInInspector]
    public SelectableObjects focusedObject;


    //used for mode change
    private bool canGetThumbstick = true;
    private float axisThreshold = 0.75f;

    //variables for handling whiteboard interaction with wall
    [HideInInspector]
    public bool pivotIsWhiteBoard = false;
    private bool pointing = false;
   

    void Awake () {
        Instance = this;
        UpdateUI(false);
        selectedObjects = new List<SelectableObjects>();
    }
	
	// Update is called once per frame
	void Update () {

        PerformRaycast(); //perform raycast regardless of mode
        CheckPointingAndRenderLine();
        CheckForKeyInputs();
	}


    private void PerformRaycast()
    {
        Ray ray = new Ray(RaycastObject.position, RaycastObject.right);
        if (Physics.Raycast(ray, out hit))
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

   
    private void CheckPointingAndRenderLine()
    {
        pointing = !OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);

        if (pointing)
        {
            lr.SetPosition(0, RaycastObject.position);
            lr.SetPosition(1, RaycastObject.position + RaycastObject.right * 15);
        }
        else
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }

    }

    private void HandleModeChangeInput()
    {
        Vector2 r = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //debug.text = "x: " + r.x + "  y:" + r.y;
        if (canGetThumbstick)
        {
            if (Mathf.Abs(r.x) > axisThreshold  )
            {
                canGetThumbstick = false;
                if(r.x < 0f)
                {
                    // thumstick: left
                    activeSelectionMode = SelectionMode.Manipulate_Raycast;
                }
                else
                {
                    //thumstick:right
                    activeSelectionMode = SelectionMode.HandGrab;
                }
                UpdateUI(true);
                SetCollidersActive(true);
            }
            else if (Mathf.Abs(r.y) > axisThreshold)
            {
                canGetThumbstick = false;
                if (r.y > 0f)
                {
                    // thumstick: top
                    activeSelectionMode = SelectionMode.Selection;
                }
                else
                {
                    //thumstick: bottom
                    activeSelectionMode = SelectionMode.Teleport;
                }
                UpdateUI(true);
                SetCollidersActive(true);
            }
        }
        else
        {
            //wait for reset here
            if(r.x == 0f && r.y == 0f)
            {
                canGetThumbstick = true;
            }
        }

    }

    private void CheckForKeyInputs()
    {
        HandleModeChangeInput();

        if (activeSelectionMode != SelectionMode.Selection)
            return;

        if (OVRInput.GetUp(selectKeyTouch))
        {
            HandleSelection();
        }
    }

    //meat and bread of selecting and deselecting objects
    private void HandleSelection()
    {
        if (!focusedObject)
            return;

        if (PivotTransform == null) //this is the first object selected, check whether the object is whiteboard or not
            pivotIsWhiteBoard = (focusedObject.alignWithWall) ? true : false;


        if (pivotIsWhiteBoard && !focusedObject.alignWithWall)
            return;

        if (!pivotIsWhiteBoard && focusedObject.alignWithWall)
            return;

        if (!focusedObject.selected) //if focused object is not in a group
        {
            selectedObjects.Add(focusedObject);
            if(selectedObjects.Count == 1)
            {
                PivotTransform = focusedObject.transform;
                PivotTransform.SetParent(null);
                RootTransform.SetParent(PivotTransform);
                focusedObject.Start_Interaction(true);
            }
            else
            {
                focusedObject.transform.SetParent(RootTransform);
                focusedObject.Start_Interaction(false);
            }
        }
        else //if focused object is  already in a group
        {
     
            //do nothing if pivot is whiteboard and trying to add non-whiteboard objects to the group
            if (pivotIsWhiteBoard && !focusedObject.alignWithWall)
                return;

            selectedObjects.Remove(focusedObject);
            focusedObject.Start_Interaction();
            focusedObject.transform.SetParent(null);

            if (PivotTransform == focusedObject.transform)
            {
                RootTransform.SetParent(null);
                PivotTransform = null;
                if(!(RootTransform.childCount == 0)){
                    PivotTransform = RootTransform.GetChild(0);
                    RootTransform.GetChild(0).SetParent(null);
                    RootTransform.SetParent(PivotTransform);
                }
               
                if (PivotTransform)
                {
                    PivotTransform.GetComponent<SelectableObjects>().SetMaterialColor("_Color", Color.red);
                }
            }


        }
        
    }

    public void UpdateUI(bool updateOthers)
    {
        if (activeSelectionMode == SelectionMode.Selection)
        {
            T_selectionMode.color = S_SelectedColor;
            T_teleportMode.color = S_DefaultColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_DefaultColor;
            tp.enabled = false;
            mr.enabled = false;
            hg.enabled = false;
           
        }
        else if (activeSelectionMode == SelectionMode.Manipulate_Raycast)
        {
            T_teleportMode.color = S_DefaultColor;
            T_selectionMode.color = S_DefaultColor;
            T_raycastMode.color = S_SelectedColor;
            T_freeformMode.color = S_DefaultColor;
            tp.enabled = false;
            mr.enabled = true;
            hg.enabled = false;

        }
        else if (activeSelectionMode == SelectionMode.HandGrab)
        {
            T_teleportMode.color = S_DefaultColor;
            T_selectionMode.color = S_DefaultColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_SelectedColor;
            tp.enabled = false;
            mr.enabled = false;
            hg.enabled = true;

        }
        else if (activeSelectionMode == SelectionMode.Teleport)
        {
            T_teleportMode.color = S_SelectedColor;
            T_selectionMode.color = S_DefaultColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_DefaultColor;
            tp.enabled = true;
            mr.enabled = false;
            hg.enabled = false;

        }
        else if(activeSelectionMode == SelectionMode.None)
        {
            T_teleportMode.color = S_DefaultColor;
            T_selectionMode.color = S_DefaultColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_DefaultColor;
            tp.enabled = false;
            mr.enabled = false;
            hg.enabled = false;
           
        }

        if (updateOthers)
        {
            rts.mode = RightThumbStickModes.None;
            rts.UpdateUI(false);
        }

    }

    public void ManipulationDone()
    {
        selectedObjects.Clear();
        PivotTransform = null;
        while(RootTransform.childCount != 0)
        {
            RootTransform.GetChild(0).SetParent(null);
        }
    }

    public void SetCollidersActive(bool active)
    {
        foreach(SelectableObjects a in selectedObjects)
        {
            a.col.enabled = active;
        }
    }

    public void SetPivotToCurrentlyPointedObject(Transform t)
    {
        if (PivotTransform == t)
            return;
        RootTransform.SetParent(null);
        PivotTransform.GetComponent<SelectableObjects>().SetMaterialColor("_Color", Color.blue);
        PivotTransform.SetParent(RootTransform);
        PivotTransform = t;
        RootTransform.SetParent(PivotTransform);
        PivotTransform.GetComponent<SelectableObjects>().SetMaterialColor("_Color", Color.red);
    }


    public void reset()
    {
        selectedObjects.Clear();
        RootTransform.SetParent(null);
        PivotTransform = null;
       
    }

}

public enum SelectionMode
{
    Selection, Manipulate_Raycast, HandGrab, Teleport, None
}

