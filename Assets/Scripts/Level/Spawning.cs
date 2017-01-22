using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
    const float SPAWN_MIN = 45f;
    const float SPAWN_MAX = 55f;

    const float HI_SPAWN_HEIGHT = 1.5f;
    const float MID_SPAWN_HEIGHT = 1.25f;
    const float BASS_SPAWN_HEIGHT = 1f;
    const float AMP_SPAWN_HEIGHT = 1.5f;

    public GameObject Hi;
    public GameObject Mid;
    public GameObject Bass;
    public GameObject Amp;
    /*
    private float hiTimer;
    private float hiBaseDelay = 1f;
    private float hiRange = 1f;
    private float hiDelay;

    private float midTimer;
    private float midBaseDelay = 1.5f;
    private float midRange = 2f;
    private float midDelay;

    private float bassTimer;
    private float bassBaseDelay = 4f;
    private float bassRange = 4f;
    private float bassDelay;

    private float ampTimer;
    private float ampBaseDelay = 5f;
    private float ampRange = 8f;
    private float ampDelay;
    */

    private float hiTimer;
    private float hiBaseDelay = 3f; //2
    private float hiRange = 2.5f; //1.5
    private float hiDelay;

    private float midTimer;
    private float midBaseDelay = 6f; //4.5
    private float midRange = 4.5f; //2.5
    private float midDelay;

    private float bassTimer;
    private float bassBaseDelay = 12f; // 8
    private float bassRange = 7f;//3
    private float bassDelay;

    private float ampTimer;
    private float ampBaseDelay = 20f;//15
    private float ampRange = 10f; //2
    private float ampDelay; 

    private float difficulty = 0;
    private float diffCoef = 0;
    const float MAX_DIFF = 2.7f;
    // Use this for initialization
    void Start () {
        hiTimer = midTimer = bassTimer = ampTimer = Time.time;
        hiDelay = Random.Range(hiBaseDelay, hiBaseDelay + hiRange);
        bassDelay = Random.Range(bassBaseDelay, bassBaseDelay + bassRange);
        midDelay = Random.Range(midBaseDelay, midBaseDelay + midRange);
        ampDelay = Random.Range(ampBaseDelay, ampBaseDelay + ampRange);
    }
	
	// Update is called once per frame
	void Update () {
        difficulty = Mathf.Log10(PlayerControl.LiveTime) - 0.1f;
        diffCoef = 1f - (difficulty / MAX_DIFF);
        diffCoef = Mathf.Clamp(diffCoef, 0f, 1f);

        hiBaseDelay = 1f + 2f * diffCoef; //2
        hiRange =  1f + 1.5f * diffCoef; //1.5
        midBaseDelay = 1.5f + 4.5f * diffCoef; //4.5
        midRange = 2f + 2.5f * diffCoef; //2.5
        bassBaseDelay = 4 + 8f * diffCoef; // 8
        bassRange = 4f + 3f * diffCoef;//3
        ampBaseDelay = 5 + 15f * diffCoef;//15
        ampRange = 8 + 2f * diffCoef; //2

	    if(Time.time - hiTimer >= hiDelay && PlayerControl.LiveTime > 20f)
        {
            Vector3 spawnHiPos = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * Random.Range(SPAWN_MIN, SPAWN_MAX);
            spawnHiPos.y = HI_SPAWN_HEIGHT;
            //Spawn
            Instantiate(Hi, spawnHiPos, Quaternion.identity);
            //Reset Timer
            hiTimer = Time.time;

            //Reset Delay
            hiDelay = Random.Range(hiBaseDelay, hiBaseDelay + hiRange);
        }

        if (Time.time - midTimer >= midDelay)
        {
            Vector3 spawnMidPos = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * Random.Range(SPAWN_MIN, SPAWN_MAX);
            spawnMidPos.y = MID_SPAWN_HEIGHT;
            //Spawn
            Instantiate(Mid, spawnMidPos, Quaternion.identity);
            //Reset Timer
            midTimer = Time.time;
            //Reset Delay
            midDelay = Random.Range(midBaseDelay, midBaseDelay + midRange);
        }

        if(Time.time - bassTimer >= bassDelay && PlayerControl.LiveTime > 45f)
        {
            Vector3 spawnBassPos = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * Random.Range(SPAWN_MIN, SPAWN_MAX);
            spawnBassPos.y = BASS_SPAWN_HEIGHT;
            //Spawn
            Instantiate(Bass, spawnBassPos, Quaternion.identity);
            //Reset Timer
            bassTimer = Time.time;
            //Reset Delay
            midDelay = Random.Range(bassBaseDelay, bassBaseDelay + bassRange);
        }

        if (Time.time - ampTimer >= ampDelay && PlayerControl.LiveTime > 60f)
        {
            Vector3 spawnAmpPos = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * Random.Range(SPAWN_MIN, SPAWN_MAX);
            spawnAmpPos.y = AMP_SPAWN_HEIGHT;
            //Spawn
            Instantiate(Amp, spawnAmpPos, Quaternion.identity);
            //Reset Timer
            ampTimer = Time.time;
            //Reset Delay
            ampDelay = Random.Range(ampBaseDelay, ampBaseDelay + ampRange);
        }
    }
}
