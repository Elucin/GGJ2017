using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIAmp : AIBase {
    List<GameObject> Ampees;
	// Use this for initialization
	protected override void Start () {
        Ampees = new List<GameObject>();
        base.Start();
        Speed = 4.5f;
        Health = 125;
	}

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Enemy"))
        {
            Ampees.Add(c.gameObject);
            AIBase aib = c.gameObject.GetComponent<AIBase>();
            aib.Amp();
        }
    }

    void OnTriggerExit(Collider c)
    {
        if(c.CompareTag("Enemy"))
        {
            Ampees.Remove(c.gameObject);
            AIBase aib = c.gameObject.GetComponent<AIBase>();
            aib.DeAmp();
        }
    }

    protected override void Death()
    {
        foreach (GameObject g in Ampees)
        {
            if(g != null)
                g.GetComponent<AIBase>().DeAmp();
        }

        base.Death();
    }
}
