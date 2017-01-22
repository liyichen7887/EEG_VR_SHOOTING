using UnityEngine;
using System.Collections;

public class TestingOnly : MonoBehaviour {
    public KeyCode button;
    public TilesReset tr;
	// Use this for initialization
	void Start () {
        tr = GetComponent<TilesReset>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(button))
        {
            tr.resetEverything();
        }
	}
}
