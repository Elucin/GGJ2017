using UnityEngine;
using System.Collections;

public class Escape : MonoBehaviour {

	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
	}
}
