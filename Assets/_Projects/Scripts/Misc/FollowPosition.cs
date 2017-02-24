using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{

    public Transform toFollow;
    public bool ignoreX = false;
    public bool ignoreY = false;
    public bool ignoreZ = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = toFollow.position;
        if (ignoreX) pos.x = transform.position.x;
        if (ignoreY) pos.y = transform.position.y;
        if (ignoreZ) pos.z = transform.position.z;
        this.transform.position = pos;
    }
}
