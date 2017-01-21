using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Image HealthBar;
    public Terminus terminus;
	// Use this for initialization
	void Start () {
        HealthBar = GameObject.Find("TerminusHealthBar").GetComponent<Image>();
        terminus = GameObject.Find("Terminus").GetComponent<Terminus>();
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.fillAmount = terminus.Health / 100f;
	}
}
