using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDotSFX : MonoBehaviour {

    public AudioClip bigdot;

    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = bigdot;
    }

    void OnCollisionEnter()
    {
        GetComponent<AudioSource>().Play();
    }
}
