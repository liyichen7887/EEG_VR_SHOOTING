using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour {
    public Transform player;
    public Vector3 checkPoint;
    public LoadCheckPoints loader;
    private int index = 0;
    public DroneController dc;
    private float distance = 0;
    private UnityEngine.UI.Text showDis;
    
    // Use this for initialization
    void Start () {
		loader = GameObject.Find("Checkpoint Loader").GetComponent<LoadCheckPoints>();
        dc = GameObject.Find("Player").GetComponent<DroneController>();
        showDis = GetComponent<Text>();
        showDis.text = "" + distance;

    }

    // Update is called once per frame
    void Update () {
        index = dc.nextTargetCheckPoint;
        checkPoint = loader.posOfCheckPointNumber(index);
        distance = calculateDis(checkPoint);
        showDis.text = "" + distance;
    }
    float calculateDis(Vector3 checkpoint) {
        distance = Vector3.Distance(player.position, checkpoint);

        return distance;
    }
}
