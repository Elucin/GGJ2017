﻿using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
    const float SPAWN_MIN = 45f;
    const float SPAWN_MAX = 55f;

    const float HI_SPAWN_HEIGHT = 1.5f;
    const float MID_SPAWN_HEIGHT = 1.25f;
    const float BASS_SPAWN_HEIGHT = 1.5f;
    const float AMP_SPAWN_HEIGHT = 1.5f;

    public GameObject Hi;
    public GameObject Mid;
    public GameObject Bass;
    public GameObject Amp;

    private float hiTimer;
    private float hiBaseDelay = 3f;
    private float hiRange = 2f;
    private float hiDelay;

    private float midTimer;
    private float midBaseDelay = 5f;
    private float midRange = 3f;
    private float midDelay;

    private float bassTimer;
    private float bassBaseDelay = 10f;
    private float bassRange = 5f;
    private float bassDelay;

    private float ampTimer;
    private float ampBaseDelay = 20f;
    private float ampRange = 5f;
    private float ampDelay;

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
	    if(Time.time - hiTimer >= hiDelay)
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

        if(Time.time - bassTimer >= bassDelay)
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

        if (Time.time - ampTimer >= ampDelay)
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