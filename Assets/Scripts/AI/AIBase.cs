using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour
{
    [UnityEngine.SerializeField]
    protected float Health;

    [UnityEngine.SerializeField]
    protected float Speed;

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
}