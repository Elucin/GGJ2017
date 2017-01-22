using UnityEngine;
using System.Collections;

public class DoBeatAnim : MonoBehaviour {
    
    Animator anim;
    void OnEnable()
    {
        BeatTest.onBeat += DoBeat;
    }

    void OnDisable()
    {
        BeatTest.onBeat -= DoBeat;
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    void DoBeat()
    {
        anim.SetTrigger("Beat");
    }
}
