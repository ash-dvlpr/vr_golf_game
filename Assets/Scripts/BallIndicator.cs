using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIndicator : MonoBehaviour
{
    public static BallIndicator sharedInstance;
    
    [SerializeField] private Transform ballPosition;

    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject particleSystem;
    private AudioSource indicatorAudioSource;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this);
        }
        else
        {
            sharedInstance = this;
            //DontDestroyOnLoad(gameObject);
        }

        indicatorAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        TurnOn();
    }

    public void TurnOn()
    {
        transform.position = ballPosition.position;
        cylinder.SetActive(true);
        particleSystem.SetActive(true);
        indicatorAudioSource.Play();
    }

    public void TurnOff()
    {
        cylinder.SetActive(false);
        particleSystem.SetActive(false);
        indicatorAudioSource.Stop();
    }
}
