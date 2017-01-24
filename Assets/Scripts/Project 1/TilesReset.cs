using UnityEngine;
using UnityEngine.UI;

public class TilesReset : MonoBehaviour {

    public Text t;
    public float timeRequiredForReset = 2.0f;
    public Image widgetImage;
    public EyeGaze eg;
    public BrickSpawner spawner;


    private float elapsedTime;
	// Use this for initialization
	void Start () {
        elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        float x = this.transform.localEulerAngles.x;
       // float y = this.transform.localEulerAngles.y;
      //  float z = this.transform.localEulerAngles.z;
       // string a = x + "  " + y   + "      T: " + elapsedTime;
        

        if(x > 270.0f && x < 320.0f )
        {
            
            elapsedTime += Time.deltaTime;
 
            
        }
        else
        {
            elapsedTime = 0.0f;
        }

        if(elapsedTime >= timeRequiredForReset)
        {
            elapsedTime = 0.0f;
            eg.ResetCannonBalls();
            spawner.resetWall();

        }
        float pctComplete = elapsedTime / timeRequiredForReset;
        widgetImage.fillAmount = pctComplete;
        t.text = "Pct: " + (pctComplete*100) + "%";
    }

    public void resetEverything()
    {
        
        elapsedTime = 0.0f;
        eg.ResetCannonBalls();
        spawner.resetWall();

        spawner.resetWall();
    }

}
