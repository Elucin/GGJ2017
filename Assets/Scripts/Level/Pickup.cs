using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

    Rigidbody rb;

    void OnEnable()
    {
        BeatTest.onBeat += Bounce;
    }

    void OnDisable()
    {
        BeatTest.onBeat -= Bounce;
    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision c)
    {
        if (c.transform.CompareTag("Player"))
        {
            if (name.Contains("Slow"))
                Powerups.SlowDown();
            else if (name.Contains("Speed"))
                Powerups.SpeedUp();
            else if (name.Contains("Power"))
                Powerups.PowerUp();

            DoDestroy();
        }
    }

    void DoDestroy()
    {
        Destroy(gameObject);
    }

    void Bounce()
    {
        rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }
}
