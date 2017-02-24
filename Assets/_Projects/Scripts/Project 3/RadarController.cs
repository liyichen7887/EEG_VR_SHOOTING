using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {
    public List<GameObject> radarObjects;
    public List<GameObject> borderObjects;
    public float switchDistance;
    public Transform helpTransform;
    private GameObject checkpointLoader;
    private LoadCheckPoints loadScript;
	// Use this for initialization
	void Start () {
        checkpointLoader = GameObject.Find("Checkpoint Loader");
        if (checkpointLoader) {
            loadScript = checkpointLoader.GetComponent<LoadCheckPoints>();
            radarObjects = loadScript.radarObjects;
            borderObjects = loadScript.borderObjects;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < radarObjects.Count; i++) {
            if (new Vector3(radarObjects[i].transform.position.x - transform.position.x, 0,
                    radarObjects[i].transform.position.z - transform.position.z).magnitude > switchDistance)
            {
                Transform radarTransform = radarObjects[i].transform;
                radarTransform.position.Set(radarTransform.position.x, 0, radarTransform.position.z);
                helpTransform.LookAt(radarTransform);
                //borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                borderObjects[i].layer = 13;
                radarObjects[i].layer = 14;
            }
            else {
                borderObjects[i].layer = 14;
                radarObjects[i].layer = 13;
            }
        }
	}

    void makeReachedCPInvisible() {

    }
}
