using UnityEngine;
using System.Collections;

public class HammerTime : MonoBehaviour {

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            Destroy(gameObject);
        }
    }

	void OnCollisionEnter(Collision c)
    {
        if(c.transform.CompareTag("Enemy"))
        {
            c.gameObject.GetComponent<AIBase>().TakeDamage(40f + 25f * (1 - anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1));
        }
    }
}
