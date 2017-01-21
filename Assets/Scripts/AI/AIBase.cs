using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour
{
    const float BASS_SPEED = 2.5f;
    const float MID_SPEED = 5f;
    const float HI_SPEED = 10f;

    [UnityEngine.SerializeField]
    protected float Health;

    public float Speed;
    public float initialSpeed;

    public int SoundDamage;

    Vector3 originalScale;
    protected Vector3 Direction;

    void OnEnable()
    {
        BeatTest.onBeat += Pop;
    }

    void OnDisable()
    {
        BeatTest.onBeat -= Pop;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        if(name.Contains("Hi"))
        {
            initialSpeed = HI_SPEED;
        }
        else if(name.Contains("Mid"))
        {
            initialSpeed = MID_SPEED;
        }
        else if(name.Contains("Bass"))
        {
            initialSpeed = BASS_SPEED;
        }

        originalScale = transform.localScale;
        Direction = -transform.position.normalized;
        Direction.y = 0f;
        transform.LookAt(Vector3.zero + Vector3.up * transform.position.y);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 newPos = transform.position + Direction * (Speed / 100);
        newPos.y = transform.position.y;
        transform.position = newPos;

        if (Health <= 0)
            Death();

        if (Powerups.SlowedDown)
            Speed = initialSpeed * Powerups.SLOW_COEFFICIENT;
        else
            Speed = initialSpeed;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    protected void Death()
    {
        //Instantiate Particle Effect
        Destroy(gameObject);
    }

    void Pop()
    {
        transform.localScale = originalScale;
        transform.localScale *= 1.5f;
        StopAllCoroutines();
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        while(transform.localScale.magnitude > originalScale.magnitude)
        {
            float scale = transform.localScale.x;
            scale -= Time.deltaTime * 2f;
            scale = Mathf.Clamp(scale, originalScale.x, transform.localScale.x);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.transform.CompareTag("Player"))
        {
            c.gameObject.GetComponent<PlayerControl>().CallTimeOut();
            TakeDamage(50f);
        }
    }
}