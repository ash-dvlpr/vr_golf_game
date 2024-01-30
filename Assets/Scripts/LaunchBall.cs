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
        if (other.CompareTag(targetTag))
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
            rBall.velocity = /*velocity*/projectedVelocity;
            
            BallIndicator.sharedInstance.TurnOff();
            
            GameManager.sharedInstance.UpdateCurrentHits();
            GameManager.sharedInstance.DisplayScore();
        }
    }
}
