using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSFX : MonoBehaviour {

    public AudioClip bumper;

	// Use this for initialization
	void Start ()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = bumper;
    }
	
	void OnCollisionEnter()
    {
        GetComponent<AudioSource>().Play();
    }
}
