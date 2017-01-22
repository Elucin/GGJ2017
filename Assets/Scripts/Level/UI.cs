using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Image HealthBar;
    public Terminus terminus;
    public Text timer;
    private float millisecondsCount;
    private int minuteCount;
	// Use this for initialization
	void Start () {
        HealthBar = GameObject.Find("TerminusHealthBar").GetComponent<Image>();
        terminus = GameObject.Find("Terminus").GetComponent<Terminus>();
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.fillAmount = terminus.Health / 100f;
        string minutes;
        string seconds;
        string milliseconds;
        if (minuteCount <= 9)
        {
            minutes = "0" + minuteCount.ToString();
        }
        else
        {
            minutes = minuteCount.ToString();
        }

        if((int)PlayerControl.LiveTime <= 9)
        {
            seconds = "0" + ((int)PlayerControl.LiveTime).ToString();
        }
        else
        {
            seconds = ((int)PlayerControl.LiveTime).ToString();
        }

        if((PlayerControl.LiveTime - (int)PlayerControl.LiveTime) * 100f <= 9)
        {
            milliseconds = "0" + ((PlayerControl.LiveTime - (int)PlayerControl.LiveTime) * 100f).ToString("F0");
        }
        else
            milliseconds = ((PlayerControl.LiveTime - (int)PlayerControl.LiveTime) * 100f).ToString("F0");
        //float milliseconds = Mathf.Round((PlayerControl.LiveTime - (Mathf.Round(PlayerControl.LiveTime * 100f)/100f) * 100f)) / 100f;

        timer.text = minutes + ":" + seconds + ":" + milliseconds;

	}
}
