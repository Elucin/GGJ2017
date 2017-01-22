using UnityEngine;
using System.Collections;

public class DestroyAudio : MonoBehaviour {

    public AudioClip[] clip;
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (source.clip == null)
        {
            source.clip = clip[Random.Range(0, clip.Length)];
            source.Play();
        }
        else if (!source.isPlaying)
            Destroy(gameObject);
	}
}
