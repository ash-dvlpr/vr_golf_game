using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIndicator : MonoBehaviour
{
    public static BallIndicator sharedInstance;
    
    [SerializeField] private Transform ballPosition;

    private MeshRenderer indicatorRender;
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

        indicatorRender = GetComponent<MeshRenderer>();
        indicatorAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        transform.position = new Vector3(ballPosition.position.x, ballPosition.position.y + 1f, ballPosition.position.z);
    }

    public void TurnOn()
    {
        transform.position = new Vector3(ballPosition.position.x, ballPosition.position.y + 1f, ballPosition.position.z);
        indicatorRender.enabled = true;
        indicatorAudioSource.Play();
    }

    public void TurnOff()
    {
        indicatorRender.enabled = false;
        indicatorAudioSource.Stop();
    }
}
