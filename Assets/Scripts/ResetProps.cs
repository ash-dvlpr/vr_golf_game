using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProps : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.sharedInstance.ResetBall();
        }
    }
}
