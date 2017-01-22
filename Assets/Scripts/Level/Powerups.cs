using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

    public static bool SlowedDown = false;
    private static float slowTimer;
    const float SLOW_TIME = 5f;
    public static float SLOW_COEFFICIENT = 0.15f;

    public static bool SpedUp = false;
    private static float speedTimer;
    const float SPEED_TIME = 7f;
    public static float SPEED_COEFFICIENT = 0.75f;

    public static bool PoweredUp = false;
    private static float powerTimer;
    const float POWER_TIME = 10f;

	// Update is called once per frame
	void Update () {
        if (SlowedDown && Time.time - slowTimer >= SLOW_TIME)
        {
            SlowedDown = false;
        }

        if (SpedUp && Time.time - speedTimer >= SPEED_TIME)
        {
            SpedUp = false;
        }

        if (PoweredUp && Time.time - powerTimer >= POWER_TIME)
        {
            PoweredUp = false;
        }
    }

    public static void SlowDown()
    {
        slowTimer = Time.time;
        SlowedDown = true;
    }

    public static void SpeedUp()
    {
        speedTimer = Time.time;
        SpedUp = true;
    }

    public static void PowerUp()
    {
        powerTimer = Time.time;
        PoweredUp = true;
    }
}
