using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : SelectableObjects
{

  //  public float yValue = 3.05f;

    private void OnEnable()
    {
        Vector3 pos = transform.position;
        pos.y = yValue;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public override void Start_Interaction()
    {

    }

}
