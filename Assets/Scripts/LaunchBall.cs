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
            Debug.Log("Plap plap plap");
            GameManager.sharedInstance.currentHitNumber++;
            
            Vector3 collisionPos = clubCollider.ClosestPoint(other.transform.position);
            Vector3 collisionNormal = other.transform.position - collisionPos;
            Vector3 projectedVelocity = Vector3.Project(velocity, collisionNormal);
            
            Rigidbody rBall = other.attachedRigidbody;
            rBall.velocity = /*velocity*/projectedVelocity;
        }
    }
}
