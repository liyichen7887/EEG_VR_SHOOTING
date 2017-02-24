using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timeLeft = 10.00f;
    UnityEngine.UI.Text startCountDown;
    public bool timeElapsed = false;
    private AudioSource beep;
  //  private AudioClip source;
 
    // Use this for initialization
    void Start () {
        beep = GetComponent<AudioSource>();
        startCountDown = GetComponent<Text>();
        startCountDown.text = "Starting in \n" + timeLeft;
        InvokeRepeating("CountDown", 1,1);
    }
	
	// Update is called once per frame
    
	void CountDown () {
        if (timeLeft > 0)
        {
            beep.Play();
            timeLeft -= 1.0f;
            startCountDown.text = "Starting in \n" + ("" + timeLeft);

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
