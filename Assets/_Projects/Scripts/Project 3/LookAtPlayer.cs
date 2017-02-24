using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private Transform toLookAt;
    private void Start()
    {
        toLookAt = DroneController.Instance.transform;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(toLookAt);
	}
}
