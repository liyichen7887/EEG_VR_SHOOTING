using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour {


    void OnCollisionEnter(Collision col)
    {
        string s = col.gameObject.name;
        Debug.Log("Collided with " + s);
    }

}
