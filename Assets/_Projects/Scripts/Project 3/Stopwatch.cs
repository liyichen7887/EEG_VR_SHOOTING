using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stopwatch : MonoBehaviour {
    float time = 0.00f;
    UnityEngine.UI.Text count;
    private GameObject timerUI;
    private Timer timerScript;
    // Use this for initialization
    void Start () {
        count = GetComponent<Text>();
        count.text = "" + time + ".00";
        timerUI = GameObject.Find("CountDown");
        timerScript = timerUI.GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timerScript.timeElapsed)
        {
            time += Time.deltaTime;
            count.text = "" + time;
        }
            

    }
}
