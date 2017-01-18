using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RaycastSelector : MonoBehaviour {

    public Text text;

    private GameObject selected;
    private RaycastHit hit;
    private Ray ray;

  //  private Camera camera;

    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            string objectName = objectHit.name;
            text.text = "Raycast Result: " + objectName;
        }
        else
        {
            text.text = "----------";
        }
    }

   /* void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 50, 50), "*", "box");
       // GUI.
    }
    */
}
