using UnityEngine;
using System.Collections;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;
    public int numRows = 10;
    public int bricksPerRow = 5;
    public float radius = 10.0f;


    private float circumfence;
    private Brick[] bricks;
    private float brickWidth;
    private float brickHeight;
    private GameObject parentGameObject;
    private Transform parentTransform;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 200; //limit FPS to 100
        if (!brickPrefab)
            Debug.LogError(" Brick Prefab not set");
        brickWidth = brickPrefab.transform.localScale.x;
        brickHeight = brickPrefab.transform.localScale.y;
        spawnBricks();
       // StartCoroutine(spawnBricks());
        parentGameObject = new GameObject("All Bricks");
        parentTransform = parentGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {


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
            float additionalRotationOffset = (i % 2 == 0) ? brickWidth / 0.5f : 0;
            for (int j = 0; j < bricksPerRow; ++j)
            {

                GameObject brick = Instantiate(brickPrefab) as GameObject;
                Transform t = brick.transform;
                t.position = new Vector3(0f, 0f, radius);
                
                t.RotateAround(Vector3.zero, Vector3.up, rotationOffset * j + additionalRotationOffset );
                Vector3 brickNewPostion = t.position;
                brickNewPostion.y += yOffset * i;
                t.position = brickNewPostion;
                brick.GetComponent<Brick>().spawner = this;
                brick.transform.parent = parentTransform;
              //  yield return new WaitForSeconds(0f);
            }
            
        }
    }



    /// <summary>
    /// used to reset transform of all the bricks
    /// </summary>
    public void resetWall()
    {

    }

}
