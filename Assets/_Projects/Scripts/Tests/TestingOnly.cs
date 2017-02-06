using UnityEngine;
using System.Collections;

public class TestingOnly : MonoBehaviour {
    public float speed = 200f;
	// Use this for initialization
	void Start () {
      //  tr = GetComponent<TilesReset>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            cube.transform.localScale = new Vector3(0.05f, .05f, .05f);
            Rigidbody rb = cube.AddComponent<Rigidbody>() as Rigidbody;
            rb.useGravity = false;
            rb.AddForce(transform.forward* speed);
            

        }
	}
}
