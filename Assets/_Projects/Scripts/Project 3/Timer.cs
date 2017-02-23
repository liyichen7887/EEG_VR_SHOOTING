using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    float timeLeft = 10.00f;
    UnityEngine.UI.Text startCountDown;
    public bool timeElapsed = false;
 
    // Use this for initialization
    void Start () {
        startCountDown = GetComponent<Text>();
        startCountDown.text = "Starting in \n" + timeLeft;
    }
	
	// Update is called once per frame
	void Update () {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            startCountDown.text = "Starting in \n" + ("" + timeLeft).Substring(0,4);
        }
        else
        {
            startCountDown.text = "";
            timeHasElapsed();
        }
    }

    //If the game controller receives this signal from the timer, it will end the game
    void timeHasElapsed()
    {
        timeElapsed = true;
    }
}
