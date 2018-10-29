using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSFX : MonoBehaviour {

    public AudioClip cheering;

    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = cheering;
    }

    void OnCollisionEnter()
    {
        GetComponent<AudioSource>().Play();
    }
}
