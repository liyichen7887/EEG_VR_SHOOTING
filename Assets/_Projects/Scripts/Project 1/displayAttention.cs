using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayAttention : MonoBehaviour {

    public int attention;
    public GameObject mainCam;
    private DisplayData displayData;
    private Text text;
    private EyeGaze eyeGaze;
    public float time;
	// Use this for initialization
	void Start () {
        displayData = mainCam.GetComponent<DisplayData>();
        eyeGaze = mainCam.GetComponent<EyeGaze>();
        attention = displayData.getAttention();
        time = 0.0f;
        text = GetComponent<Text>();
        text.text =  "Time Elapsed: " + time + "\nAttention: " + attention + "\nLevel: " + eyeGaze.level;
        InvokeRepeating("timer", 0, 0.1f);
    }

    void timer() {
        if (eyeGaze.level > 1)
            time += 0.1f;
    }
	// Update is called once per frame
	void Update () {
        attention = displayData.getAttention();

        text.text = "Time Elapsed: " + time + "\nAttention: " + attention + "\nLevel: " + eyeGaze.level;
    }
}
