using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMovingForward : MonoBehaviour {

    public KeyCode pauseKey = KeyCode.P;
    public float speed = 3.0f;
    public bool pause = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(pauseKey)) pause = !pause;
        if (pause) return;

        Vector3 p = this.transform.position;
        p += (this.transform.forward * speed);
        this.transform.position = p;

	}
}
