using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMode
{

}


public class ObjectSpawner : MonoBehaviour {

    public GameObject[] objects;
    public Transform SpawnPoint;

    private List<GameObject> spawnedItems;

    void Start()
    {
        spawnedItems = new List<GameObject>();
    }


    void Update()
    {

    }

}

