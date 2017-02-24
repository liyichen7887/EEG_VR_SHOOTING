using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour {
    public Transform player;
    public Vector3 checkPoint;
    public GameObject loader;
    private int index = 0;
    public GameObject dc;
    private float distance = 0;
    private UnityEngine.UI.Text showDis;
    public bool gameover = false;
    
    // Use this for initialization
    void Start () {
        showDis = GetComponent<Text>();
        showDis.text = "" + distance;

    }

    // Update is called once per frame
    void Update () {
        if (gameover) return;
        index = dc.GetComponent<DroneController>().nextTargetCheckPoint;
        checkPoint = loader.GetComponent<LoadCheckPoints>().posOfCheckPointNumber(index);
        distance = calculateDis(checkPoint);
        showDis.text = "" + distance;
    }
    float calculateDis(Vector3 checkpoint) {
        distance = Vector3.Distance(player.position, checkpoint);

        return distance;
    }
}
