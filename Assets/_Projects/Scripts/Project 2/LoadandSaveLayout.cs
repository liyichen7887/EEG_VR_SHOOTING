using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadandSaveLayout : MonoBehaviour
{
    public ObjectSpawner os;
    public string relativePath = "/_Projects";
    private string absolutePath;
    public AudioSource audS;
    public AudioClip saveClip;
    public AudioClip loadClip;
    // Use this for initialization
    void Start()
    {
        absolutePath = Application.dataPath + relativePath;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            Save();
        if (Input.GetKeyDown(KeyCode.M))
            Load();
    }

    public void Save()
    {
        if(!File.Exists(absolutePath))
        {
           File.Create(absolutePath);
        }
        audS.PlayOneShot(saveClip);
        StreamWriter sw = new StreamWriter(absolutePath);
        foreach (GameObject o in ObjectSpawner.Instance.spawnedItems)
        {
            Vector3 pos = o.transform.position;
            Vector3 angles = o.transform.rotation.eulerAngles;
            string s = o.GetComponent<SelectableObjects>().Objectname;
            s += "," + pos.x + "," + pos.y + "," + pos.z + "," + angles.x + "," + angles.y + "," + angles.z;
            sw.WriteLine(s);
        }
        sw.Close();
    }

    public void Load()
    {
        
        if (!File.Exists(absolutePath))
        {
            Debug.LogError("Path/File doesn't exist");
            return;
        }
        char[] delimiter = { ',' };
        StreamReader sr = new StreamReader(absolutePath);
        audS.PlayOneShot(loadClip);
        os.DestroyAll();

        string line = sr.ReadLine();
        while (line != null)
        {

            string[] args = line.Split(delimiter);
            if (args.Length != 7)
            {
                Debug.LogError("File Parse Error, Incorrect Length, expected:7, actual:" + args.Length);
                return;
            }
            string objType = args[0];
            Vector3 pos = new Vector3(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
            Quaternion rotation = Quaternion.Euler(float.Parse(args[4]), float.Parse(args[5]), float.Parse(args[6]));
            ObjectSpawner.Instance.Spawn(objType, pos, rotation);
            line = sr.ReadLine();
        }
        
    }

}
