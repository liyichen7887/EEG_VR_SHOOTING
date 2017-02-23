using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class LeapGestureTest : MonoBehaviour {
    private LeapProvider provider;

    // Use this for initialization
    void Start () {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        if (!provider)
            Debug.LogError("Leap Provider not found");
    }
	
	// Update is called once per frame
	void Update () {
        Frame frame = provider.CurrentFrame;
        int counter = 1;
        foreach(Hand hand in frame.Hands)
        {
            
            List<Finger> fingers =  hand.Fingers;
            Debug.Log(" Hand #" + counter + " has " + fingers.Count + " fingers");
            counter++;
        }
	}
}
