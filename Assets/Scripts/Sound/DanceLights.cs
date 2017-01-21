using UnityEngine;
using System.Collections;

public class DanceLights : MonoBehaviour {

    void OnEnable()
    {
        BeatTest.onBeat += Strobe;
    }

    void OnDisable()
    {
        BeatTest.onBeat -= Strobe;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Strobe()
    {
        GameObject.FindObjectOfType<Light>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
