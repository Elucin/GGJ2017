using UnityEngine;
using System.Collections;

public class OrbitCam : MonoBehaviour {
    float zOffset = -55f;
    const float INI_ZOFFSET = -25;
    const float CAM_HEIGHT = 18f;
    Transform playerTrans;
	// Use this for initialization
	void Start () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void OnPreRender () {
        //transform.LookAt(Vector3.zero);
        zOffset = INI_ZOFFSET - playerTrans.position.magnitude;

        transform.position = playerTrans.position.normalized * -zOffset + Vector3.up * CAM_HEIGHT;

        Vector3 GroundPos = playerTrans.position - transform.position;
        GroundPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(GroundPos);
        rotation.eulerAngles += new Vector3(30, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
}
