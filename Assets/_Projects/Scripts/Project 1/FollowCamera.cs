
using UnityEngine;

/// <summary>
///
/// </summary>
public class FollowCamera : MonoBehaviour {

    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;
    private Transform camT;
	void Start () {
        camT = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = new Vector3(camT.position.x + xOffset, 0.01f+ yOffset, camT.position.z + zOffset);
        this.transform.position = newPos;
     //   this.transform.rotation = camT.rotation;
	}
}
