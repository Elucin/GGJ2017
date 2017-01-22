using UnityEngine;
using System.Collections;

public class GoodVibrations : MonoBehaviour {


    void OnEnable()
    {
        BeatTest.onBeat += Next;
    }

    void OnDisable()
    {
        BeatTest.onBeat -= Next;
    }
    public Animator[] anims;
    int range = 0;
	// Use this for initialization


    void Next()
    {
        anims[range].SetTrigger("Beat");
        range++;
        if (range == anims.Length)
            range = 0;
    }
}
