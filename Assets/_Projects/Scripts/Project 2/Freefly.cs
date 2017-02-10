using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freefly : MonoBehaviour
{

    public Transform ObjectToMove;
    public Transform pivot;
    public float speed = 1.0f;
    public OVRInput.Button pauseButton = OVRInput.Button.Two;
    private bool pause = false;
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(pauseButton))
        {
            pause = !pause;
        }

        if (pause) return;

        ObjectToMove.position += pivot.right * Time.deltaTime * speed;
    }
}
