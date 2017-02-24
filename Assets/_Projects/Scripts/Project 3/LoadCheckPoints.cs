using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class LoadCheckPoints : MonoBehaviour
{

    public TextAsset coordFile;
    public GameObject checkPointPrefab;
    public GameObject checkPointMinMapPrefab;
    public GameObject Player;
    private float scale = 0.0254f;
    public List<Transform> checkPoints;
    public List<GameObject> radarObjects;
    public List<GameObject> borderObjects;
    public static int totalNumCheckPoint = 0;
    // Use this for initialization
    void Start()
    {
        checkPoints = new List<Transform>();
        char[] lineDelim = { '\n' };
        string s = coordFile.text;
        string[] coords = s.Split(lineDelim);
        string[] xyz = new string[3];
        // int[] coord = new int[3];
        char[] spaceDelim = {' '};
        int counter = 0;

        foreach (string c in coords)
        {
            ++counter;
            xyz = c.Split(spaceDelim);
            Vector3 coord = new Vector3(Int32.Parse(xyz[0])* scale, Int32.Parse(xyz[1]) * scale, Int32.Parse(xyz[2])* scale);
            GameObject go = Instantiate(checkPointPrefab) as GameObject;
           // GameObject j = Instantiate(checkPointMinMapPrefab) as GameObject;
          //  GameObject k = Instantiate(checkPointMinMapPrefab) as GameObject;
           // j.name = "radar #" + counter;
           // k.name = "border #" + counter;
           // radarObjects.Add(j);
           // borderObjects.Add(k);
            go.name = "Checkpoint #" + counter;
            CheckPoint cp = go.GetComponent<CheckPoint>();
            cp.ID = counter;
            cp.lcp = this;
            cp.t.text = ""+counter;
            checkPoints.Add(go.transform);
            go.transform.position = coord;
            //j.transform.position = coord;
            //k.transform.position = coord;

            /*   GameObject sphere = Instantiate(checkPointMinMapPrefab) as GameObject;
               sphere.transform.position = coord;
               sphere.layer = 12;*/
        }
        totalNumCheckPoint = checkPoints.Count;
        Player.transform.position = checkPoints[0].position;
    }

    /// <summary>
    /// return position of checkpoint number n, starting at 1
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public Vector3 posOfCheckPointNumber(int n)
    {
        return checkPoints[n - 1].position;
    }


}
