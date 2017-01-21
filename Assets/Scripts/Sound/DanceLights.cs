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
        Light[] lights = GameObject.FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            l.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            if(l.name.Contains("Spot"))
            {
                int en = Random.Range(0, 2);
                l.enabled = en == 1;
            }
        }
    }
}
