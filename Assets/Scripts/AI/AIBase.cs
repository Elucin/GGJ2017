using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour
{
    [UnityEngine.SerializeField]
    protected float Health;

    [UnityEngine.SerializeField]
    protected float Speed;

    public int SoundDamage;

    protected Vector3 Direction;

    // Use this for initialization
    protected virtual void Start()
    {
        Direction = -transform.position.normalized;
        Direction.y = 0f;
        transform.LookAt(Vector3.zero + Vector3.up * transform.position.y);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += Direction * (Speed / 100);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}