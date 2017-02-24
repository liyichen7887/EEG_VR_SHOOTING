using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtNextCheckPoint : MonoBehaviour {

    
    public Transform Player;
    public DroneController dc;
    public LoadCheckPoints lcp;
    private Transform target;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int n = dc.nextTargetCheckPoint;
        if (n > lcp.checkPoints.Count) return;

        target = lcp.checkPoints[n];
      //  Debug.Log(" target checkPoint #:" + n);
        Vector3 lookPos = target.position - Player.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        this.transform.rotation = rotation;
        transform.LookAt(target.position);
    }
}
