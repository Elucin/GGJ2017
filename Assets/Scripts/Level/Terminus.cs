using UnityEngine;
using System.Collections;

public class Terminus : MonoBehaviour {
    public float Health = 100f;

	void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Enemy"))
        {
            Health -= c.GetComponent<AIBase>().SoundDamage;
            StartCoroutine(DelayDestroy(c.gameObject));
        }
    }

    IEnumerator DelayDestroy(GameObject g)
    {
        yield return new WaitForSeconds(1f);
        Destroy(g);
    }
}
