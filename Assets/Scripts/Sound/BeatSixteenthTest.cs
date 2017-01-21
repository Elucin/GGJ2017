using UnityEngine;
using System.Collections;
using SynchronizerData;

public class BeatSixteenthTest : MonoBehaviour {

    public delegate void BeatAction();
    public static event BeatAction onBeat;

    private BeatObserver beatObserver;
    void Start()
    {
        beatObserver = GetComponent<BeatObserver>();
    }


    void Update()
    {
        if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
        {
            if (onBeat != null)
                onBeat();
        }
    }
}
