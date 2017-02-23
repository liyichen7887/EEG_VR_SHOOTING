using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;


public class LeapGestureTest : MonoBehaviour
{
    private LeapProvider provider;
    public LineRenderer l1;
    public LineRenderer l2;
    // Use this for initialization
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        if (!provider)
            Debug.LogError("Leap Provider not found");
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = provider.CurrentFrame;
        if(frame.Hands.Count == 0)
        {
            l1.SetPosition(0, Vector3.zero);
            l1.SetPosition(1, Vector3.zero);
        }

        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                Bone[] bones = hand.Fingers[1].bones;
                Bone bone = bones[3];

                Vector c = bone.Center;
                Vector d = bone.Direction;
                Vector3 center = new Vector3(c.x, c.y, c.z);
                Vector3 direction = new Vector3(d.x, d.y, d.z);
                Debug.DrawRay(center, direction * 100f, Color.black);
                l1.SetPosition(0, center);
                l1.SetPosition(1, center + direction * 500f);
            }
        }
    }
}
