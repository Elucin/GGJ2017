using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Next());
	}
	
    IEnumerator Next()
    {
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
