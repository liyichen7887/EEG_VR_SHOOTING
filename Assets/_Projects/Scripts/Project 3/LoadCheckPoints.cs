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
    private float scale = 0.0254f;
    public List<Transform> checkPoints;

    // Use this for initialization
    void Start()
    {
        checkPoints = new List<Transform>();
        char[] lineDelim = { '\n' };
        string s = coordFile.text;
        string[] coords = s.Split(lineDelim);
        string[] xyz = new string[3];
        // int[] coord = new int[3];
        char[] spaceDelim = { ' ' };
        int counter = 0;

        foreach (string c in coords)
        {
            ++counter;
            xyz = c.Split(spaceDelim);
            Vector3 coord = new Vector3(Int32.Parse(xyz[0])* scale, Int32.Parse(xyz[1]) * scale, Int32.Parse(xyz[2])* scale);
            GameObject go = Instantiate(checkPointPrefab) as GameObject;
            GameObject sphere = Instantiate(checkPointMinMapPrefab) as GameObject;
            sphere.transform.position = coord;
            sphere.layer = 12;
            CheckPoint cp = go.GetComponent<CheckPoint>();
            cp.ID = counter;
            cp.lcp = this;
            checkPoints.Add(go.transform);
            go.transform.position = coord;
        
        }
    }


}
