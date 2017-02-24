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
        CheckPoint prevCheckPoint = null;
        foreach (string c in coords)
        {
            ++counter;
            xyz = c.Split(spaceDelim);
            Vector3 coord = new Vector3(Int32.Parse(xyz[0])* scale, Int32.Parse(xyz[1]) * scale, Int32.Parse(xyz[2])* scale);
            GameObject go = Instantiate(checkPointPrefab) as GameObject;
            go.name = "Checkpoint #" + counter;
            CheckPoint cp = go.GetComponent<CheckPoint>();
            cp.ID = counter;
            cp.lcp = this;
            cp.t.text = ""+counter;
            checkPoints.Add(go.transform);
            go.transform.position = coord;

            if(counter == 1)
            {
                cp.audS.volume = 0.0f;
            }

            if (prevCheckPoint)
            {
                float offset = 1.0f;
                Vector3 p = prevCheckPoint.transform.position;
                p.y -= offset;
                cp.lr.SetPosition(0, p);

                p = cp.transform.position;
                p.y -= offset;
                cp.lr.SetPosition(1, p);
            }
            prevCheckPoint = cp;
        }
        totalNumCheckPoint = checkPoints.Count;
        Player.transform.position = checkPoints[0].position;
        Vector3 lookPos = checkPoints[1].transform.position - checkPoints[0].transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        Player.transform.rotation = rotation;


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
