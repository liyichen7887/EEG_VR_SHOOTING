using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotTransformTest : MonoBehaviour {

    public KeyCode moveKey = KeyCode.Z;
    public KeyCode spawnKey = KeyCode.X;
    public KeyCode deleteKey = KeyCode.C;
    public Transform toMove;
    public Vector3 vec;

    private List<GameObject> list;
	// Use this for initialization
	void Start () {
        list = new List<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(moveKey))
        {
            toMove.position = vec;
        }

        if (Input.GetKeyDown(spawnKey))
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = vec;
        }
        if (Input.GetKeyDown(deleteKey))
        {
            foreach (GameObject o in list)
            {
                Destroy(o);
            }
        }
    }
}
