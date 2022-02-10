using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    
    AudioSource audioSource;
    public AudioSource winMusic;

    void Start()
    {
        winMusic.Play();
    }
}
