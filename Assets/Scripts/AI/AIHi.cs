using UnityEngine;
using System.Collections;

public class AIHi : AIBase {

    const float MAX_AGGRO = 15f;
    const float AGGRO_THRESHOLD = 5f;
    const float AGGRO_BUILD_RATE = 2f;
    const float AGGRO_DROPOFF_RATE = -6f;

    private bool m_Aggro = false;
    private bool m_inRange = false;
    private float m_AggroMeter = 0f;
    NavMeshAgent meshAgent;
    Transform playerTrans;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        meshAgent = GetComponent<NavMeshAgent>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), GameObject.Find("Terminus").GetComponent<CapsuleCollider>());
	}
	
	// Update is called once per frame
	protected override void Update () {
        if(!m_Aggro)
        {
            base.Update();
        }
        else
        {
            meshAgent.SetDestination(playerTrans.position);
        }

        if(m_inRange)
        {
            m_AggroMeter += Time.deltaTime * (AGGRO_BUILD_RATE + (40 - Vector3.Distance(playerTrans.position, transform.position)) / 40);
        }
        /*
        else
        {
            m_AggroMeter += Time.deltaTime * (AGGRO_DROPOFF_RATE + (40 - Vector3.Distance(playerTrans.position, transform.position)) / 40);
        } */

        m_AggroMeter = Mathf.Clamp(m_AggroMeter, 0f, MAX_AGGRO);

        if (m_AggroMeter >= AGGRO_THRESHOLD && !m_Aggro)
            GoAggro();
        else if(m_AggroMeter < AGGRO_THRESHOLD && m_Aggro)
            GoPassive();
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            m_inRange = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            m_inRange = false;
        }
    }

    void GoAggro()
    {
        meshAgent.enabled = true;
        m_AggroMeter += 2f;
        m_Aggro = true;
    }

    void GoPassive()
    {
        meshAgent.enabled = false;
        Direction = -transform.position.normalized;
        Direction.y = 0f;
        transform.LookAt(Vector3.zero + Vector3.up * transform.position.y);
        m_Aggro = false;
    }
}
