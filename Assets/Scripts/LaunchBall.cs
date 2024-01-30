using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private Vector3 previousPosition;
    private Vector3 velocity;
    private Collider clubCollider;
    
    [SerializeField] private AudioSource slowHitSound;
    [SerializeField] private AudioSource hardHitSound;

    private void Awake()
    {
        clubCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            GameManager.sharedInstance.currentHitNumber++;
            
            Vector3 collisionPos = clubCollider.ClosestPoint(other.transform.position);
            Vector3 collisionNormal = other.transform.position - collisionPos;
            Vector3 projectedVelocity = Vector3.Project(velocity, collisionNormal);
            
            Rigidbody rBall = other.attachedRigidbody;
            rBall.velocity = projectedVelocity;

            //Debug.Log("Golpe X: " + projectedVelocity.x + " Golpe Y: " + projectedVelocity.y + " Golpe Z: " + projectedVelocity.z);
            if ((projectedVelocity.x < -1.1f || projectedVelocity.x > 1.1f) || (projectedVelocity.z < -1.1f || projectedVelocity.z > 1.1f))
            {
                slowHitSound.Play();
            }
            else
            {
                hardHitSound.Play();
            }
            
            BallIndicator.sharedInstance.TurnOff();
            
            GameManager.sharedInstance.UpdateCurrentHits();
            GameManager.sharedInstance.DisplayScore();
        }
    }
}
