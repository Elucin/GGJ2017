using UnityEngine;
using System.Collections;

public class DestroyAnimation : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            Destroy(gameObject);
        }
    }
}
