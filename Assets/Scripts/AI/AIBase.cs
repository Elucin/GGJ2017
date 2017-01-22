using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIBase : MonoBehaviour
{
    const float BASS_SPEED = 2.5f;
    const float MID_SPEED = 5f;
    const float HI_SPEED = 10f;
    const float AMP_SPEED = 4.5f;

    const float BASS_HEALTH = 200;
    const float MID_HEALTH = 100f;
    const float HI_HEALTH = 25f;
    const float AMP_HEALTH = 125f;

    const float SMALL_HEALTHBOOST = 25f;
    const float LARGE_HEALTHBOOST = 50f;

    public bool Amped = false;
    public GameObject deathrattle;
    public GameObject deathParticles;

    protected float Health;

    public float Speed;
    public float initialSpeed;
    public float initialHealth;

    public int SoundDamage;

    Vector3 originalScale;
    float originalY;
    protected Vector3 Direction;

    public Image Healthbar;

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
        if(GetComponent<SphereCollider>())
            Physics.IgnoreCollision(GetComponent<SphereCollider>(), GameObject.Find("Terminus").GetComponent<CapsuleCollider>());
        Healthbar = GetComponentInChildren<Image>();
        if(name.Contains("Hi"))
        {
            initialSpeed = HI_SPEED;
            initialHealth = Health = HI_HEALTH;
        }
        else if(name.Contains("Mid"))
        {
            initialSpeed = MID_SPEED;
            initialHealth = Health = MID_HEALTH;
        }
        else if(name.Contains("Bass"))
        {
            initialSpeed = BASS_SPEED;
            initialHealth = Health = BASS_HEALTH;
        }
        else if(name.Contains("Amp"))
        {
            initialSpeed = AMP_SPEED;
            initialHealth = Health = AMP_HEALTH;
        }

        originalScale = transform.localScale;
        originalY = transform.position.y;
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

        HealthBar();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    protected virtual void Death()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Instantiate(deathrattle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Pop()
    {
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        transform.position += Vector3.up * 0.8f;
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.transform.CompareTag("Player"))
        {
            c.gameObject.GetComponent<PlayerControl>().CallTimeOut();
            TakeDamage(50f);
        }
    }

    public void Amp()
    {
        if (!Amped)
        {
            if (name.Contains("Hi"))
            {
                Health += SMALL_HEALTHBOOST;
                initialHealth += SMALL_HEALTHBOOST;
            }
            else if (name.Contains("Mid"))
            {
                Health += SMALL_HEALTHBOOST;
                initialHealth += SMALL_HEALTHBOOST;
            }
            else if (name.Contains("Bass"))
            {
                Health += LARGE_HEALTHBOOST;
                initialHealth += LARGE_HEALTHBOOST;
            }
            else if (name.Contains("Amp"))
            {
                Health += LARGE_HEALTHBOOST;
                initialHealth += LARGE_HEALTHBOOST;
            }
            Amped = true;
        }
        
    }

    public void DeAmp()
    {
        if (Amped)
        {
            Debug.Log(name + " Deamped");
            if (name.Contains("Hi"))
            {
                Health -= SMALL_HEALTHBOOST;
                initialHealth -= SMALL_HEALTHBOOST;
            }
            else if (name.Contains("Mid"))
            {
                Health -= SMALL_HEALTHBOOST;
                initialHealth -= SMALL_HEALTHBOOST;
            }
            else if (name.Contains("Bass"))
            {
                Health -= LARGE_HEALTHBOOST;
                initialHealth -= LARGE_HEALTHBOOST;
            }
            else if (name.Contains("Amp"))
            {
                Health -= LARGE_HEALTHBOOST;
                initialHealth -= LARGE_HEALTHBOOST;
            }

            Amped = false;
            Health = Mathf.Clamp(Health, 1f, 400f);
        }
    }

    void HealthBar()
    {
        Healthbar.fillAmount = Health / initialHealth;
        if (Health / initialHealth < 1f)
            Healthbar.enabled = true;
        else
            Healthbar.enabled = false;        
    }
}