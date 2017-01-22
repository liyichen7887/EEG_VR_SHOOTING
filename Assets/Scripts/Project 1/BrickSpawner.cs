using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;
    public int numRows = 10;
    public int bricksPerRow = 5;
    public float radius = 10.0f;


    private float circumfence;
    private List<Brick> bricks;
    private float brickWidth;
    private float brickHeight;
    private GameObject parentGameObject;
    // private GameObject[] rowParentGameObject;

    private Transform parentTransform;
    private static int NUM_BRICKS;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 200; //limit FPS to 100
        if (!brickPrefab)
            Debug.LogError(" Brick Prefab not set");

        bricks = new List<Brick>();
        brickWidth = brickPrefab.transform.localScale.x;
        brickHeight = brickPrefab.transform.localScale.y;
        parentGameObject = new GameObject();
        parentGameObject.name = "Bricks";
        parentTransform = parentGameObject.transform;
        NUM_BRICKS = 0;
        spawnBricks();
    }


    /// <summary>
    /// Spawn bricks using giving radius and numRows
    /// </summary>
    private void spawnBricks()
    {

        circumfence = Mathf.PI * 2.0f * radius;
        int bricksPerRow = (int)(circumfence / brickWidth);
        Debug.Log("Bricks Per Row: " + bricksPerRow);
        float rotationOffset = 360.0f / (float)bricksPerRow;
        float yOffset = brickHeight;

        for (int i = 0; i < numRows; ++i)
        {
            GameObject rowParent = new GameObject();
            rowParent.transform.parent = parentTransform;
            rowParent.name = "Row " + i;
            float additionalRotationOffset = (i % 2 == 0) ? brickWidth / 0.5f : 0;
            for (int j = 0; j < bricksPerRow; ++j)
            {
                GameObject brick = Instantiate(brickPrefab) as GameObject;
                Transform t = brick.transform;
                t.position = new Vector3(0f, 0f, radius);
                t.RotateAround(Vector3.zero, Vector3.up, rotationOffset * j + additionalRotationOffset);
                Vector3 brickNewPostion = t.position;
                brickNewPostion.y += yOffset * i + 0.002f;
                t.position = brickNewPostion;

                //set member variables for future use
                Brick b = brick.GetComponent<Brick>();
                b.spawner = this;
                b.ID = (++NUM_BRICKS);
                b.rowNumber = i;
                b.positionInRow = j;
                b.defaultPosition = t.position;
                b.defaultRotation = t.rotation;


                brick.transform.parent = rowParent.transform;
                bricks.Add(b);
            }
        }
    }



    /// <summary>
    /// used to reset transform of all the bricks
    /// </summary>
    public void resetWall()
    {
        foreach (Brick b in bricks)
        {

            b.rb.useGravity = false;
            b.bc.enabled = false;
          
        }
        foreach (Brick b in bricks)
        {
            b.reset();
            b.rb.useGravity = true;
            b.bc.enabled = true;

        }
    }



}
