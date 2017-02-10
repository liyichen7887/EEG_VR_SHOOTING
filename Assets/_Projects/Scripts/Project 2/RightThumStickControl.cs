using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightThumStickControl : MonoBehaviour {

    
    public RightThumbStickModes mode = RightThumbStickModes.None;
    public SelectObject so;
    [Header("Load And Save Settings")]
    public LoadandSaveLayout loadAndSave;
    public OVRInput.Button loadButton = OVRInput.Button.One;
    public OVRInput.Button saveButton = OVRInput.Button.Two;


    [Header("UI")]
    public Text T_left;
    public Text T_right;
    public Text T_up;
    public Text T_bottom;
    public Color S_DefaultColor = Color.black;
    public Color S_SelectedColor = Color.green;
    //used for mode change
    private bool canGetThumbstick = true;
    private float axisThreshold = 0.75f;
    // Update is called once per frame

    void Start()
    {
        UpdateUI(false);
    }

    void Update () {
        HandleJoystickInput();
        HandleButtonInput();
    }


    private void HandleJoystickInput()
    {
        Vector2 r = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //debug.text = "x: " + r.x + "  y:" + r.y;
        if (canGetThumbstick)
        {
            if (Mathf.Abs(r.x) > axisThreshold)
            {
                canGetThumbstick = false;
                if (r.x < 0f)
                {
                    // thumstick: left
                    mode = RightThumbStickModes.LoadAndSave;
                }
                else
                {
                    //thumstick:right
                    mode = RightThumbStickModes.Mode2;

                }
                UpdateUI(true);
            }
            else if (Mathf.Abs(r.y) > axisThreshold)
            {
                canGetThumbstick = false;
                if (r.y > 0f)
                {
                    // thumstick: top
                    mode = RightThumbStickModes.Mode3;

                }
                else
                {
                    //thumstick: bottom
                    mode = RightThumbStickModes.Mode4;
                }
                UpdateUI(true);

            }
        }
        else
        {
            //wait for reset here
            if (r.x == 0f && r.y == 0f)
            {
                canGetThumbstick = true;
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick))
        {
            mode = RightThumbStickModes.None;
            UpdateUI(false);
        }

    }

    private void HandleButtonInput()
    {
        if(mode == RightThumbStickModes.LoadAndSave)
        {
            CheckInputForLoadAndSave();
        }
    }

    private void CheckInputForLoadAndSave()
    {
        if (OVRInput.GetUp(loadButton))
        {
            loadAndSave.Load();
        }

        if (OVRInput.GetUp(saveButton))
        {
            loadAndSave.Save();
        }
    }


    public void UpdateUI(bool updateOthers)
    {
        if(mode == RightThumbStickModes.LoadAndSave)
        {
            
            T_left.color = S_SelectedColor;
            T_right.color = S_DefaultColor;
            T_up.color = S_DefaultColor;
            T_bottom.color = S_DefaultColor;
        }
        else if(mode == RightThumbStickModes.Mode2)
        {
            
            T_left.color = S_DefaultColor;
            T_right.color = S_SelectedColor;
            T_up.color = S_DefaultColor;
            T_bottom.color = S_DefaultColor;
        }
        else if (mode == RightThumbStickModes.Mode3)
        {
            
            T_left.color = S_DefaultColor;
            T_right.color = S_DefaultColor;
            T_up.color = S_SelectedColor;
            T_bottom.color = S_DefaultColor;
        }
        else if (mode == RightThumbStickModes.Mode4)
        {
            
            T_left.color = S_DefaultColor;
            T_right.color = S_DefaultColor;
            T_up.color = S_DefaultColor;
            T_bottom.color = S_SelectedColor;
        }
        else if (mode == RightThumbStickModes.None)
        {
            
            T_left.color = S_DefaultColor;
            T_right.color = S_DefaultColor;
            T_up.color = S_DefaultColor;
            T_bottom.color = S_DefaultColor;
        }

        if (updateOthers)
        {
            so.activeSelectionMode = SelectionMode.None;
            so.UpdateUI(false);
        }

    }

}

public enum RightThumbStickModes
{
    LoadAndSave, Mode2, Mode3, Mode4, None
}