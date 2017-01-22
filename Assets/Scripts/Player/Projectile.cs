﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    const int VELOCITY = 150;
    const int DAMAGE = 25;
    const float LIFETIME = 1f;

    float timer;

	// Use this for initialization
	void Start () {
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
	}

    void OnCollisionEnter(Collision c)
    {
        if(c.transform.CompareTag("Enemy"))
        {
            c.gameObject.GetComponent<AIBase>().TakeDamage(DAMAGE + Powerups.PoweredUp.GetHashCode() * 25f);
        }
        Destroy(gameObject);
    }
}