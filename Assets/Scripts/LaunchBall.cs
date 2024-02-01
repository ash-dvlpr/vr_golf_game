using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    [SerializeField] private string targetTag;

    private static int BUFFER_LENGTH = 3;
    private readonly Queue<Vector3> velocityBuffer = new();

    private Vector3 previousPosition;
    private Collider clubCollider;
    
    [SerializeField] private AudioSource slowHitSound;
    [SerializeField] private AudioSource hardHitSound;

    [SerializeField] private Rigidbody ballRB;

    private void Awake()
    {
        clubCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        velocityBuffer.Clear();
        for(int i = 0; i < BUFFER_LENGTH; i++) {
            velocityBuffer.Enqueue(Vector3.zero);
        }
    }

    private void Update()
    {
        // Calculate this frame's velocity
        var velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        // Store the velocity
        velocityBuffer.Enqueue(velocity);
        velocityBuffer.Dequeue(); // Pop oldest velocity
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && ballRB.velocity == Vector3.zero)
        {
            // Calculate average compound velocity
            var _compoundVelocity = velocityBuffer.ToList().Aggregate((a, b) => a + b);
            _compoundVelocity /= velocityBuffer.Count;

            // Apply velocity
            GameManager.sharedInstance.currentHitNumber++;
            
            Vector3 collisionPos = clubCollider.ClosestPoint(other.transform.position);
            Vector3 collisionNormal = other.transform.position - collisionPos;
            Vector3 projectedVelocity = Vector3.Project(_compoundVelocity, collisionNormal);
            
            Rigidbody rBall = other.attachedRigidbody;
            rBall.velocity = projectedVelocity;

            //Debug.Log("Golpe X: " + projectedVelocity.x + " Golpe Y: " + projectedVelocity.y + " Golpe Z: " + projectedVelocity.z);
            
            //if ((projectedVelocity.x < -1.1f || projectedVelocity.x > 1.1f) || (projectedVelocity.z < -1.1f || projectedVelocity.z > 1.1f))
            if (projectedVelocity.sqrMagnitude < 2f)
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
