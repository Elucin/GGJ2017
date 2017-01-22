using UnityEngine;
using System.Collections;

public class PowerupSpawning : MonoBehaviour {

    const float SPAWN_Y = 0.8f;

    float spawnTimer;
    float spawnDelay;
    float minSpawnTime = 30f;
    float spawnVariance = 10f;
    public GameObject[] powerups;

	// Use this for initialization
	void Start () {
        spawnTimer = Time.time;
        spawnDelay = minSpawnTime + Random.Range(0, spawnVariance);
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - spawnTimer > spawnDelay)
        {
            Vector3 pos = GetSpawnPos();
            while (pos.magnitude < 8)
                pos = GetSpawnPos();
            Instantiate(powerups[Random.Range(0, 3)], pos, Quaternion.identity);
            spawnTimer = Time.time;
            //Spawn
        }
	}

    Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = Random.insideUnitCircle * 30f;
        spawnPos.z = spawnPos.y;
        spawnPos.y = SPAWN_Y;
        return spawnPos;
    }
}
