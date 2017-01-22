using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Submit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
	}
}
