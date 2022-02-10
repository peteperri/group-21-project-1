using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScene : MonoBehaviour
{
    
    AudioSource audioSource;
    public AudioSource loseMusic;

    void Start()
    {
        loseMusic.Play();
    }
}
