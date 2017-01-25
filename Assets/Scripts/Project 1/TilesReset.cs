using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TilesReset : MonoBehaviour {

    public Text t;
    public float timeRequiredForReset = 2.0f;
    public Image widgetImage;
    public EyeGaze eg;
    public BrickSpawner spawner;
    public AudioClip resetSound;

    private AudioSource audioS;
    private float elapsedTime;
	// Use this for initialization
	void Start () {
        elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        float x = this.transform.localEulerAngles.x;
      //  float y = this.transform.localEulerAngles.y;
      //  float z = this.transform.localEulerAngles.z;
       // string a = x + "  " + y   + "      T: " + elapsedTime;
      //  t.text = a;

        if (x > 280.0f && x < 300.0f )
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
      //  t.text = "Pct: " + (pctComplete*100) + "%";
    }

    public void resetEverything()
    {

        audioS.PlayOneShot(resetSound);
        elapsedTime = 0.0f;
        eg.ResetCannonBalls();
        spawner.resetWall();

        spawner.resetWall();
    }

}
