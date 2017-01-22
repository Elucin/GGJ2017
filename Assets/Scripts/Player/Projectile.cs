using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public AudioClip[] clips;
    AudioSource source;
    const int VELOCITY = 150;
    const int DAMAGE = 35;
    const float LIFETIME = 1f;

    float timer;
    public GameObject target = null;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
        timer = Time.time;
        GetComponent<Rigidbody>().velocity = transform.forward * VELOCITY;
        if(Powerups.PoweredUp)
        {
            GetComponent<Rigidbody>().mass = 10f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - timer >= LIFETIME)
            Destroy(gameObject);
        if (target != null)
        {
            GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * 50, ForceMode.Acceleration);
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * VELOCITY;
            if (Vector3.Angle(transform.forward, target.transform.position - transform.position) > 90f)
                target = null;

        }
	}

    void OnCollisionEnter(Collision c)
    {
        if(c.transform.CompareTag("Enemy"))
        {
            c.gameObject.GetComponent<AIBase>().TakeDamage(DAMAGE + Powerups.PoweredUp.GetHashCode() * 1000f);
        }
        Destroy(gameObject);
    }
}
