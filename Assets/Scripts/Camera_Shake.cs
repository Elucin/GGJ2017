using UnityEngine;
using System.Collections;

public class Camera_Shake : MonoBehaviour {

	//public float duration = 0.5f;
	//public float magnitude = 0.1f;

	public void PlayShake(float duration = 0.5f, float magnitude = 0.1f) {
		StopAllCoroutines();
		StartCoroutine(Shake(duration, magnitude));
	}

	IEnumerator Shake(float duration, float magnitude) {
		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;			

			float percentComplete = elapsed / duration;			
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map noise to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}
}