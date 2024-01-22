using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private string targetTag = "Ball";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            GameManager.sharedInstance.GoToNextHole();
        }
    }
}
