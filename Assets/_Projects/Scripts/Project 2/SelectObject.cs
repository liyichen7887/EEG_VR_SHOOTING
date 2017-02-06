using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour {
    public static SelectObject Instance;

    public SelectionMode activeSelectionMode = SelectionMode.Selection;
    public Teleport tp;
    public ManipulateRaycast mr;

    [Header("Audio Fields")]
    public AudioSource audioS;
    public AudioClip selectSound;
    public AudioClip letGoSound;

    [Header("UI Fields")]
    public Text T_selectionMode;
    public Text T_manipulateMode;
    public Text T_teleportMode;
    public Text T_raycastMode;
    public Text T_freeformMode;
    public Color S_DefaultColor = Color.black;
    public Color S_SelectedColor = Color. green;

    [Header("Key Bindings")]
    public KeyCode switchModeKey = KeyCode.UpArrow;
    public OVRInput.Button switchModeTouch = OVRInput.Button.Three;
    public KeyCode ManipulateActionKey = KeyCode.Mouse0;
    public OVRInput.Button ManipulateActionKeyTouch = OVRInput.Button.Four;

    public KeyCode selectKey = KeyCode.Mouse0;
    public OVRInput.Button selectKeyTouch = OVRInput.Button.Four;

    //additonal variables for keeping track of selection states
   // private bool HasSelectedObjects = false;

    //used for raycasting
    private RaycastHit hit;
    private Transform camT;
    private Transform hitTransform;
   // private Vector3 hitPoint;
    private SelectableObjects focusedObject;

    
    //used for handling selection
    private List<SelectableObjects> selectedObjects;
    [HideInInspector]
    public Transform PivotTransform; 

    [Header("Misc")]
    public Transform RootTransform;
    /* when 1st item is selected, its transform will be set to PivotTransform,
     * and have RootTransform as its first child, subsequent selection will have
     * RootTransform as their parent, if the 1st item is getting deselected,
     *  
     */


    //variables for Raycast Manipulation
   // private bool Manipulating = false;
   // private Transform origPos;


    // Use this for initialization
    void Awake () {
        Instance = this;
        camT = Camera.main.transform;
        UpdateUI();
        selectedObjects = new List<SelectableObjects>();
        tp.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

        PerformRaycast(); //perform raycast regardless of mode
        CheckForKeyInputs();
	}

    private void PerformRaycast()
    {
        Debug.DrawRay(camT.position, camT.forward * 15, Color.red);
        Ray ray = new Ray(camT.position, camT.forward);
        if (Physics.Raycast(ray, out hit))
        {
            hitTransform = hit.transform;
            focusedObject = hitTransform.GetComponent<SelectableObjects>();
           // hitPoint = hit.point;
        }
        else
        {
            focusedObject = null;
            hitTransform = null;
        }
    }

    private void ManipulateObjects()
    {

    }

    private void CheckForKeyInputs()
    {
        if (Input.GetKeyDown(switchModeKey) || OVRInput.Get(switchModeTouch))
        {
            if (activeSelectionMode == SelectionMode.Selection)
            {
                activeSelectionMode = SelectionMode.Manipulate_Raycast;
                mr.enabled = true;
            }
            else if (activeSelectionMode == SelectionMode.Manipulate_Raycast)
            {
                activeSelectionMode = SelectionMode.Manipulate_TBD;
                mr.enabled = false;
            }
            else if (activeSelectionMode == SelectionMode.Manipulate_TBD)
            {
                activeSelectionMode = SelectionMode.Teleport;
                tp.enabled = true; ;
            }
            else if (activeSelectionMode == SelectionMode.Teleport)
            {
                activeSelectionMode = SelectionMode.Selection;
                tp.enabled = false;
            }
            UpdateUI();
        }


        //handles the case when player selects/deselects an object
        if (Input.GetKeyDown(selectKey) || OVRInput.Get(selectKeyTouch))
        {
            if (activeSelectionMode == SelectionMode.Selection)
            {
                HandleSelection();
            }
        }


        if (Input.GetKeyDown(ManipulateActionKey) || OVRInput.Get(ManipulateActionKeyTouch))
        {

        }
    }


    private void HandleSelection()
    {
        if (!focusedObject)
        {
            return;
        }

        if (!focusedObject.selected)
        {
            selectedObjects.Add(focusedObject);
            if(selectedObjects.Count == 1)
            {
                PivotTransform = focusedObject.transform;
                RootTransform.SetParent(PivotTransform);
                focusedObject.Start_Interaction(true);
            }
            else
            {
                focusedObject.transform.SetParent(RootTransform);
                focusedObject.Start_Interaction(false);
            }
        }
        else
        {
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


    private void UpdateUI()
    {
        if (activeSelectionMode == SelectionMode.Selection)
        {
            T_selectionMode.color = S_SelectedColor;
            T_manipulateMode.color = S_DefaultColor;
            T_teleportMode.color = S_DefaultColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_DefaultColor;
        }
        else if (activeSelectionMode == SelectionMode.Manipulate_Raycast)
        {
            T_teleportMode.color = S_DefaultColor;
            T_selectionMode.color = S_DefaultColor;
            T_manipulateMode.color = S_SelectedColor;
            T_raycastMode.color = S_SelectedColor;
            T_freeformMode.color = S_DefaultColor;
        }
        else if (activeSelectionMode == SelectionMode.Manipulate_TBD)
        {
            T_teleportMode.color = S_DefaultColor;
            T_selectionMode.color = S_DefaultColor;
            T_manipulateMode.color = S_SelectedColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_SelectedColor;
        }
        else if (activeSelectionMode == SelectionMode.Teleport)
        {
            T_teleportMode.color = S_SelectedColor;
            T_selectionMode.color = S_DefaultColor;
            T_manipulateMode.color = S_DefaultColor;
            T_raycastMode.color = S_DefaultColor;
            T_freeformMode.color = S_DefaultColor;
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

}

public enum SelectionMode
{
    Selection, Manipulate_Raycast, Manipulate_TBD, Teleport
}

