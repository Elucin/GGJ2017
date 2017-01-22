using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text time;
	// Use this for initialization
	void Start () {
        time.text = UI.minutes + " Minutes and " + UI.seconds + " Seconds";
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Submit"))
        {
            PlayerControl.LiveTime = 0f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
	}
}
