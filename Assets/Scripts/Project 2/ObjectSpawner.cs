using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMode
{

}


public class ObjectSpawner : MonoBehaviour {

    public GameObject[] prefabs;
    public GameObject[] demoGameObject;
    public Transform SpawnPoint;

    [Header("Key Bindings")]
    public KeyCode NextItem = KeyCode.RightArrow;
    public OVRInput.Button NextItemTouch = OVRInput.Button.Two;
    public KeyCode spawnKey = KeyCode.DownArrow;
    public OVRInput.Button spawnKeyTouch = OVRInput.Button.Three;

    private int currentSelection;
    private List<GameObject> spawnedItems;
    private Vector3 spawnPosition;
    void Start()
    {
        spawnedItems = new List<GameObject>();
        currentSelection = 0;
        spawnPosition = SpawnPoint.position;
    }


    void Update()
    {
        if (Input.GetKeyDown(NextItem) || OVRInput.Get(NextItemTouch))
        {
            SelectNextItem();
        }
        if (Input.GetKeyDown(spawnKey) || OVRInput.Get(spawnKeyTouch))
        {
            SpawnItem();
        }
    }

    void SelectNextItem()
    {
        currentSelection = (currentSelection + 1) % (prefabs.Length);
        for(int i=0; i < prefabs.Length; ++i)
        {
            if( i == currentSelection)
            {
                demoGameObject[i].SetActive(true);
            }
            else
            {
                demoGameObject[i].SetActive(false);
            }

        }
    }

    void SpawnItem()
    {
        GameObject obj = Instantiate(prefabs[currentSelection]) as GameObject;
        obj.transform.position = new Vector3(spawnPosition.x, obj.transform.position.y, spawnPosition.z);
        spawnedItems.Add(obj);
    }
}

