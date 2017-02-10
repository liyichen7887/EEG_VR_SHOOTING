using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(SelectObject))]
public class ObjectSpawner : MonoBehaviour {

    public static ObjectSpawner Instance;
    public SelectObject so;
    public GameObject[] prefabs;
    public GameObject[] demoGameObject;
    public Transform SpawnPoint;

    [Header("Key Bindings")]
    public KeyCode NextItem = KeyCode.RightArrow;
    public OVRInput.Button NextItemTouch = OVRInput.Button.Two;
    public KeyCode spawnKey = KeyCode.DownArrow;
    public OVRInput.Button spawnKeyTouch = OVRInput.Button.Three;


    //[HideInInspector]
    public List<GameObject> spawnedItems;
    private int currentSelection;
    private Vector3 spawnPosition;

    void Awake()
    {
        spawnedItems = new List<GameObject>();
        Instance = this;
        currentSelection = 0;
        spawnPosition = SpawnPoint.position;
    }


    void Update()
    {
        if (so.activeSelectionMode != SelectionMode.Selection)
            return;

        if (Input.GetKeyDown(NextItem) || OVRInput.GetUp(NextItemTouch))
        {
            SelectNextItem();
        }
        if (Input.GetKeyDown(spawnKey) || OVRInput.GetUp(spawnKeyTouch))
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
      //  spawnedItems.Add(obj);  //added in the start function in SelectableObject.cs
    }

    
    public void Spawn(string type, Vector3 pos, Quaternion rotation)
    {
        switch (type)
        {
            case "Desk":
                Instantiate(prefabs[0], pos, rotation);
                break;
            case "Chair":
                Instantiate(prefabs[1], pos, rotation);
                break;
            case "Cabinet":
                Instantiate(prefabs[2], pos, rotation);
                break;
            case "Locker":
                Instantiate(prefabs[3], pos, rotation);
                break;
            case "3DTV":
                Instantiate(prefabs[4], pos, rotation);
                break;
            case "Whiteboard":
                Instantiate(prefabs[5], pos, rotation);
                break;
        }

    }

    public void DestroyAll()
    {
        foreach(GameObject o in spawnedItems)
        {
            Destroy(o);
        }
    }

}

