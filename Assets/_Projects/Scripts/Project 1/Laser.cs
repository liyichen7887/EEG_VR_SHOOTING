using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(this.gameObject, 2.0f); //self destruct after 5 sec
	}
	

}
