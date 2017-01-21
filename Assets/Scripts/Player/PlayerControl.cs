using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    float xAxis;
    float yAxis;
    float xLook;
    float yLook;
  
    //Components
    Rigidbody rBody;

    //External References
    Camera mainCam;

    //Properties
    const float SPEED = 12f;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        //Input
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        xLook = Input.GetAxis("RotateX");
        yLook = Input.GetAxis("RotateY");
        Movement();
	}

    void Movement()
    {
        Vector3 fw = mainCam.transform.forward;
        fw.y = 0;
        Vector3 right = mainCam.transform.right;
        rBody.velocity = (fw * SPEED * yAxis) + (right * SPEED * xAxis) + new Vector3(0, rBody.velocity.y, 0);
        Vector3 targetDirection = fw * -yLook + right * xLook;
        if (targetDirection.magnitude > 0.15f)
        {
            Quaternion targetLook = Quaternion.LookRotation(targetDirection, Vector3.up);
            rBody.MoveRotation(targetLook);
        }
    }


}

