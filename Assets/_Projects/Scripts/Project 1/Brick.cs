using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Brick : Interactable {

   
    public BrickSpawner spawner;
    public int ID;
    public int rowNumber;
    public int positionInRow;
    public int target;

    [HideInInspector]
    public Vector3 defaultPosition;
    [HideInInspector]
    public Quaternion defaultRotation;
    private bool HitByLaser;

    public MeshRenderer r;
    public BoxCollider bc;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        HitByLaser = false;
        target = 0;
        r = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
	}
	

    public void reset()
    {

       this.transform.position = defaultPosition;
       this.transform.rotation = defaultRotation;

        if (HitByLaser)
        {
            HitByLaser = false;
            r.enabled = true;
            bc.enabled = true;
            rb.useGravity = true;
        }

    }
   
    public void OnCollisionEnter(Collision col)
    {
        
     
        if(col.gameObject.tag == "cannonball")
        {

        }
    }

    public void RespondToLaserAttack()
    {
        this.transform.position = new Vector3(-999f, -999f);
        HitByLaser = true;
        r.enabled = false;
        bc.enabled = false;
        rb.useGravity = false;
    }

}
