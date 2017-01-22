using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    const int BEAT_COUNT_FRAMES = 12;
    const float TIME_OUT = 3.0f; //Scale based on how many deaths?
    float originalY;
    public static float LiveTime = 0;

    int deathCount = 0;

    float xAxis;
    float yAxis;
    float xLook;
    float yLook;
    bool aimLock;
    bool RailgunHeld;
    bool RailgunPressed = false;
    bool canShoot = false;
    bool canShootFaster = false;
    bool HammerDown = false;
    bool HammerPressed = false;

    bool cooldown = true;
    const float COOLDOWN_LENGTH = 0.3f;
    Camera_Shake camShake;
    public static bool isInTimeOut = false;

    //Components
    Rigidbody rBody;
    LineRenderer line;
    MeshRenderer[] meshRend;
    CapsuleCollider cap;
    Animator anim;
    public Light Powered;
    public Light Sped;
    public Light Slow;
    AudioSource audioS;

    //External References
    Camera mainCam;
    public Material ghost;
    public Material defaultMat;

    //Prefabs
    public GameObject projectile;
    public GameObject railgun;
    public GameObject hammer;
    public LayerMask enemiesOnly;

    //Properties
    const float SPEED = 12f;
    public AudioClip timeOut;
    public AudioClip timeIn;

    int beatCounter = 0;

    void OnEnable()
    {
        BeatTest.onBeat += ResetBeatCount;
        BeatEighthTest.onBeat += ShootOnBeat;
        BeatSixteenthTest.onBeat += ShootFasterOnBeat;

    }

    void OnDisable()
    {
        BeatTest.onBeat -= ResetBeatCount;
        BeatEighthTest.onBeat -= ShootOnBeat;
        BeatSixteenthTest.onBeat -= ShootFasterOnBeat;
    }

    // Use this for initialization
    void Start () {
        Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(0, 9);
        Physics.IgnoreLayerCollision(8, 12);
        Physics.IgnoreLayerCollision(0, 12);
        Physics.IgnoreLayerCollision(9, 12);
        Physics.IgnoreLayerCollision(10, 12);
        originalY = transform.position.y;
        rBody = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        line = GetComponent<LineRenderer>();
        meshRend = GetComponentsInChildren<MeshRenderer>();
        cap = GetComponent<CapsuleCollider>();
        camShake = GameObject.FindObjectOfType<Camera_Shake>();
        anim = GetComponentInChildren<Animator>();
        audioS = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        //Input
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        xLook = Input.GetAxis("RotateX");
        yLook = Input.GetAxis("RotateY");
        RailgunHeld = Input.GetAxis("RailGun") > 0.2f || Input.GetButton("RailGun");
        HammerDown = Input.GetAxis("Hammer") > 0.2f || Input.GetButton("Hammer") ;
        aimLock = Input.GetButton("AimLock");

        anim.SetBool("RailgunHeld", RailgunHeld);
        anim.SetBool("Firing", Mathf.Abs(xLook) > 0.1f || Mathf.Abs(yLook) > 0.1f);
        anim.SetBool("TimeOut", isInTimeOut);
        anim.SetBool("HammerHeld", HammerDown);
        Movement();
        if (!isInTimeOut)
        {
            if (RailgunHeld && cooldown)
            {
                RailgunPressed = true;
                line.SetPosition(1, Vector3.forward * 120f);
            }
            else
                line.SetPosition(1, Vector3.zero);

            if (!RailgunHeld && RailgunPressed)
            {
                //camShake.PlayShake(0.5f, 2f);
                
                Instantiate(railgun, transform.position + transform.forward + transform.up / 2, transform.rotation);
                RailgunPressed = false;
                Railgun(transform.position + transform.up / 2, transform.forward);
                StartCoroutine(Cooldown());
            }



            if ((canShoot && !Powerups.SpedUp) || (canShootFaster && Powerups.SpedUp))
            {
                if ((Mathf.Abs(xLook) > 0.1f || Mathf.Abs(yLook) > 0.1f) && !RailgunHeld && !HammerDown)
                {
                    GameObject target = null;
                    float angle = 25f;
                    RaycastHit[] enemies = Physics.SphereCastAll(transform.position, 2.5f, transform.forward, 100f, enemiesOnly, QueryTriggerInteraction.Ignore);

                    foreach(RaycastHit r in enemies)
                    {
                        if (Vector3.Angle(transform.forward, r.transform.position - transform.position) < angle)
                        {
                            angle = Vector3.Angle(transform.forward, r.transform.position - transform.position);
                            target = r.transform.gameObject;
                        }
                    }

                    GameObject bullet = (GameObject)Instantiate(projectile, transform.position + transform.forward * 1.5f + transform.up / 2, transform.rotation) as GameObject;
                    if(target != null)
                    {
                        bullet.GetComponent<Projectile>().target = target;
                    }

                }
                canShoot = false;
                canShootFaster = false;
            }

            if (HammerDown && cooldown)
            {
                HammerPressed = true;
                line.SetPosition(1, Vector3.forward * 18);
            }
            else
            {
                if(!RailgunHeld)
                    line.SetPosition(1, Vector3.zero);
            }

            if(!HammerDown && HammerPressed)
            {
                //rBody.AddForce(-transform.forward * 2000f, ForceMode.Impulse);
                Instantiate(hammer, transform.position + transform.forward * 2, transform.rotation);
                HammerPressed = false;
                StartCoroutine(Cooldown());
            }

            beatCounter--;
            beatCounter = Mathf.Clamp(beatCounter, 0, BEAT_COUNT_FRAMES);
        }

        if (Powerups.PoweredUp)
            Powered.enabled = true;
        else
            Powered.enabled = false;

        if (Powerups.SlowedDown)
            Slow.enabled = true;
        else
            Slow.enabled = false;

        if (Powerups.SpedUp)
            Sped.enabled = true;
        else
            Sped.enabled = false;

        if (GameObject.Find("Terminus").gameObject.GetComponent<Terminus>().Health > 0)
        {
            LiveTime += Time.deltaTime;
        }
    }

    void Movement()
    {
        Vector3 fw = mainCam.transform.forward;
        fw.y = 0;
        Vector3 right = mainCam.transform.right;
        rBody.velocity = ((fw * SPEED * yAxis) + (right * SPEED * xAxis)) * (1 + Powerups.SpedUp.GetHashCode() * Powerups.SPEED_COEFFICIENT) + new Vector3(0, rBody.velocity.y, 0);
       

        Vector3 targetDirection = fw * -yLook + right * xLook;
        

        if (targetDirection.magnitude > 0.15f)
        {
            Quaternion newRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            if (RailgunHeld)
            { 
                newRotation = Quaternion.Slerp(rBody.rotation, newRotation, Time.deltaTime);   
            }

            rBody.MoveRotation(newRotation);
        }
        else if(aimLock)
            transform.localEulerAngles += transform.up * -xAxis;
    }

    IEnumerator Cooldown()
    {
        cooldown = false;
        yield return new WaitForSeconds(COOLDOWN_LENGTH);
        cooldown = true;
    }

    void Railgun(Vector3 pos, Vector3 dir)
    {
        RaycastHit[] hit = Physics.SphereCastAll(pos, 0.7f ,dir, 120f,enemiesOnly ,QueryTriggerInteraction.Ignore);
        foreach(RaycastHit h in hit)
        {
            if (h.transform.CompareTag("Enemy"))
                h.transform.GetComponent<AIBase>().TakeDamage(100f + Powerups.PoweredUp.GetHashCode() * 1000f);
        }
    }

    public void CallTimeOut()
    {
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        audioS.clip = timeOut;
        audioS.Play();
        gameObject.layer = 9;
        rBody.useGravity = false;
        foreach(MeshRenderer r in meshRend)
            r.material = ghost;
        isInTimeOut = true;
        yield return new WaitForSeconds(TIME_OUT + (0.3f * deathCount));
        audioS.clip = timeIn;
        audioS.Play();
        isInTimeOut = false;
        foreach (MeshRenderer r in meshRend)
            r.material = defaultMat;
        rBody.useGravity = true;
        cap.enabled = true;
        gameObject.layer = 11;
    }



    void ResetBeatCount()
    {
        beatCounter = BEAT_COUNT_FRAMES;
    }

    void ShootOnBeat()
    {
        canShoot = true;
    }

    void ShootFasterOnBeat()
    {
        canShootFaster = true;
    }
}

