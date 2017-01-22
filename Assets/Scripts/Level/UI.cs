using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Image HealthBar;
    public Terminus terminus;
    public Text timer;
    public Image pause;
    public AudioSource music;
    private float millisecondsCount;
    private int minuteCount = 0;
    public static string minutes;
    public static string seconds;
    public static string milliseconds;
    // Use this for initialization
    void Start () {
        HealthBar = GameObject.Find("TerminusHealthBar").GetComponent<Image>();
        terminus = GameObject.Find("Terminus").GetComponent<Terminus>();
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.fillAmount = terminus.Health / 100f;
        
        if (minuteCount <= 9)
        {
            minutes = "0" + minuteCount.ToString();
        }
        else
        {
            minutes = minuteCount.ToString();
        }

        if((int)PlayerControl.LiveTime - 60 * minuteCount <= 9)
        {
            seconds = "0" + (((int)PlayerControl.LiveTime) - 60 * minuteCount).ToString();
        }
        else
        {
            seconds = ((int)PlayerControl.LiveTime - 60 * minuteCount).ToString();
        }

        if((PlayerControl.LiveTime - (int)PlayerControl.LiveTime) * 100f <= 9)
        {
            milliseconds = "0" + ((PlayerControl.LiveTime - (int)PlayerControl.LiveTime) * 100f).ToString("F0");
        }
        else
            milliseconds = ((PlayerControl.LiveTime - (int)PlayerControl.LiveTime) * 100f).ToString("F0");
        //float milliseconds = Mathf.Round((PlayerControl.LiveTime - (Mathf.Round(PlayerControl.LiveTime * 100f)/100f) * 100f)) / 100f;

        timer.text = minutes + ":" + seconds + ":" + milliseconds;
        minuteCount = (int)(PlayerControl.LiveTime / 60f);

        if(Input.GetButtonDown("Start"))
        {
            pause.enabled = !pause.enabled;
        }
	}
}
