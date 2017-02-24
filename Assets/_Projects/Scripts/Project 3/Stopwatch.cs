using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stopwatch : MonoBehaviour {
    float time = 0.00f;
    UnityEngine.UI.Text count;
    public Timer timer;
    private bool gameOver = false;
    // Use this for initialization
    void Start () {
        count = GetComponent<Text>();
        count.text = "" + time + ".00";
      //  InvokeRepeating("startWatch",0.0f,0.001f);
    }

    private void Update()
    {
        if (gameOver) return;
        if (timer.timeElapsed)
        {

            time += Time.deltaTime;
            float truncated = (float)(Math.Truncate((double)time * 100.0) / 100.0);
            float rounded = (float)(Math.Round((double)time, 2));
            count.text = ("" + rounded);
            //  if (count.text.Length > 4)
            //count.text = count.text.Substring(0, 4);
        }
    }

    // Update is called once per frame
    void startWatch () {
        if (gameOver) return;
            if (timer.timeElapsed)
            {
               
                time += 0.001f;
                float truncated = (float)(Math.Truncate((double) time * 100.0) / 100.0);
                float rounded = (float)(Math.Round((double) time, 2));
            count.text = ("" + rounded);
          //  if (count.text.Length > 4)
                //count.text = count.text.Substring(0, 4);
            }

            

    }

   public void setGameOver(bool end) {
        gameOver = end;
    }
}
