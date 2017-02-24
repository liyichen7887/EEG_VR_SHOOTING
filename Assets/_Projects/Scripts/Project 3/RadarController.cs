using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {
    public Transform player;
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
            if (new Vector3(radarObjects[i].transform.position.x - player.position.x, 0,
                    radarObjects[i].transform.position.z - player.position.z).magnitude > switchDistance)
            {
                Transform radarTransform = radarObjects[i].transform;
                Vector3 temp = player.position;
                //player.position = new Vector3(player.position.x,0,player.position.z);
                radarTransform.position = new Vector3(radarTransform.position.x, 0, radarTransform.position.z);
                helpTransform.LookAt(radarTransform);
                borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;

                //borderObjects[i].transform.position = player.position + switchDistance * (player.position - radarTransform.position);
                Vector3 borderPosition = borderObjects[i].transform.position;
                borderObjects[i].transform.position = new Vector3(borderPosition.x,0,borderPosition.z);
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
