using UnityEngine;
using System.Collections;

public class LookAtCam : MonoBehaviour {
    Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cam.transform.position);
	}
}
