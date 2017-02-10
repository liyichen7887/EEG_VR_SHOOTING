using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uprightfly : MonoBehaviour
{

    public Transform ObjectToMove;
    public float speed = 0.05f;
    public float yMin = 1.2f;
    public float yMax = 500f;
    public OVRInput.Button revertButton = OVRInput.Button.One;
    public OVRInput.Button pauseButton = OVRInput.Button.Two;
    private bool pause = false;
    private bool revert = false;
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(revertButton))
        {
            revert = !revert;
        }

        if (OVRInput.GetUp(pauseButton))
        {
            pause = !pause;
        }

        if (pause) return;
        Vector3 pos = ObjectToMove.position;
        pos.y = (revert) ? pos.y-speed : pos.y+speed;
        if (pos.y > yMax || pos.y < yMin)
            return;

        ObjectToMove.position = pos;
    }
}
