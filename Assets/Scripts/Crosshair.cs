using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    public Texture crosshairImage;
    public Text coordResult;
    public bool hide = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //force cursor to be at center of window

    }

    /*
	void OnGUI()
    {

        if (!crosshairImage || hide)
            return;

        GUI.color = new Color(1, 1, 1, 0.8f);
        GUI.DrawTexture(new Rect((Screen.width * 0.5f) - (crosshairImage.width * 0.5f),
            (Screen.height * 0.5f) - (crosshairImage.height * 0.5f), crosshairImage.width,
            crosshairImage.height), crosshairImage);
    }
    */

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
