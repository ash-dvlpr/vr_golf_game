using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sharedInstance;

    public AudioSource gameMusic;
    public AudioSource endGameMusic;
    public AudioSource clappingSound;
    
    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(this);
        }
        else
        {
            sharedInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
}
