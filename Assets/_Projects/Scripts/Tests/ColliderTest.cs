using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour {


    void OnCollisionEnter(Collision col)
    {
        string s = col.gameObject.name;
        Debug.Log("Collided with " + s);
    }

    void OnCollisionStay(Collision col)
    {
        string s = col.gameObject.name;
        Debug.Log("Staying in collider of " + s);
    }

    void OnCollisionExit(Collision col)
    {
        string s = col.gameObject.name;
        Debug.Log("Exiting collider of " + s);
    }

}
