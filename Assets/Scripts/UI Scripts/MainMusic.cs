using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    
    AudioSource audioSource;
    public AudioSource titleMusic;

    void Start()
    {
        titleMusic.Play();
    }
}

