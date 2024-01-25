using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProps : MonoBehaviour
{
    [SerializeField] private Transform resetArea;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResetPlane"))
        {
            if (CompareTag("Ball"))
            {
                GameManager.sharedInstance.ResetBall();
            }

            if (CompareTag("Club"))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                transform.rotation = resetArea.rotation;
                transform.position = resetArea.position;
            }
        }
    }
}
