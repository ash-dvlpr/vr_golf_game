using System;
using UnityEngine;

public class HandsFullToggle : MonoBehaviour
{
    [SerializeField] private SphereCollider handCollider;
    [SerializeField] private SphereCollider oppositeHandCollider;
    [SerializeField] private bool isRightHand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Club"))
        {
            if (isRightHand)
            {
                GameManager.sharedInstance.rightHandFull = true;
            }
            else
            {
                GameManager.sharedInstance.leftHandFull = true;
            }
            oppositeHandCollider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Club"))
        {
            if (isRightHand)
            {
                GameManager.sharedInstance.rightHandFull = false;
            }
            else
            {
                GameManager.sharedInstance.leftHandFull = false;
            }
            oppositeHandCollider.enabled = true;
        }
    }
}
