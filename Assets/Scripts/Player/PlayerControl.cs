using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    const int BEAT_COUNT_FRAMES = 12;
    const float TIME_OUT = 2.5f; //Scale based on how many deaths?

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

    public static bool isInTimeOut = false;

    //Components
    Rigidbody rBody;
    LineRenderer line;
    MeshRenderer meshRend;
    CapsuleCollider cap;

    //External References
    Camera mainCam;
    public Material ghost;
    public Material defaultMat;

    //Prefabs
    public GameObject projectile;
    public GameObject railgun;
    public GameObject hammer;
    //Properties
    const float SPEED = 12f;

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
        rBody = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        line = GetComponent<LineRenderer>();
        meshRend = GetComponent<MeshRenderer>();
        cap = GetComponent<CapsuleCollider>();
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

        Movement();
        if (!isInTimeOut)
        {
            if (RailgunHeld)
            {
                RailgunPressed = true;
                line.SetPosition(1, Vector3.forward * 120f);
            }
            else
                line.SetPosition(1, Vector3.forward * 2f);

            if (!RailgunHeld && RailgunPressed)
            {
                Instantiate(railgun, transform.position + transform.forward + transform.up / 2, transform.rotation);
                RailgunPressed = false;
                Railgun(transform.position + transform.up / 2, transform.forward);
                //Debug.Log(beatCounter);
            }



            if ((canShoot && !Powerups.SpedUp) || (canShootFaster && Powerups.SpedUp))
            {
                if ((Mathf.Abs(xLook) > 0.1f || Mathf.Abs(yLook) > 0.1f) && !RailgunHeld && !HammerDown)
                {
                    GameObject bullet = (GameObject)Instantiate(projectile, transform.position + transform.forward * 1.5f + transform.up / 2, transform.rotation) as GameObject;

                }
                canShoot = false;
                canShootFaster = false;
            }

            if (HammerDown)
            {
                HammerPressed = true;
                line.SetPosition(1, Vector3.forward * 8);
            }
            else
            {
                if(!RailgunHeld)
                    line.SetPosition(1, Vector3.forward * 2f);
            }

            if(!HammerDown && HammerPressed)
            {
                Instantiate(hammer, transform.position + transform.forward / 2, transform.rotation);
                HammerPressed = false;
            }

            beatCounter--;
            beatCounter = Mathf.Clamp(beatCounter, 0, BEAT_COUNT_FRAMES);
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
            //angleH = transform.eulerAngles.y;
        }
        //else if (RailgunHeld)
        //{
            
            /*
            angleH  += xLook * 200 * Time.deltaTime;

            if (angleH > 180f)
                angleH -= 360f;
            else if (angleH < -180f)
                angleH += 360f;

            Quaternion aimRotation = Quaternion.Euler(0, angleH, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimRotation, Time.deltaTime * 30); */

        //}
        else if(aimLock)
            transform.localEulerAngles += transform.up * -xAxis;
    }

    void Railgun(Vector3 pos, Vector3 dir)
    {
        RaycastHit[] hit = Physics.RaycastAll(pos, dir, 120f);
        foreach(RaycastHit h in hit)
        {
            if (h.transform.CompareTag("Enemy"))
                h.transform.GetComponent<AIBase>().TakeDamage(1000);
                

        }
    }

    public void CallTimeOut()
    {
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        gameObject.layer = 9;
        rBody.useGravity = false;
        meshRend.material = ghost;
        isInTimeOut = true;
        yield return new WaitForSeconds(TIME_OUT + (0.2f * deathCount));
        isInTimeOut = false;
        meshRend.material = defaultMat;
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

