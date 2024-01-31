using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorManager : MonoBehaviour
{
    [SerializeField] private Material stillMaterial;
    [SerializeField] private Material movingMaterial;
    private MeshRenderer ballMesh;
    private Rigidbody ballRB;

    private void Awake()
    {
        ballMesh = GetComponent<MeshRenderer>();
        ballRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ballRB.velocity == Vector3.zero)
        {
            ballMesh.material = stillMaterial;
        }
        else
        {
            ballMesh.material = movingMaterial;
        }
    }
}
