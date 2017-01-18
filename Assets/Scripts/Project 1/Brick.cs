using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

   
    public BrickSpawner spawner;
    public int ID;
    public int rowNumber;
    public int positionInRow;

    [HideInInspector]
    public Vector3 defaultPosition;
    [HideInInspector]
    public Quaternion defaultRotation;
    private bool HasBeingHit;
	// Use this for initialization
	void Start () {
        HasBeingHit = false;
	}
	

    public void reset()
    {
        if (HasBeingHit)
        {
            string s = "Resetting brick in row " + rowNumber + " position " + positionInRow;
            Debug.Log(s);
            this.transform.position = defaultPosition;
            this.transform.rotation = defaultRotation;
        }
    }
   
    public void OnCollisionEnter()
    {
        HasBeingHit = true;
    }

}
