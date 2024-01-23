using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    
    private int currentHole;
    public int currentHitNumber;
    private List<int> previousHitNumbers;
    public TextMeshPro scoreText;

    [SerializeField] private List<Transform> startingPositions;

    [SerializeField] private Rigidbody ballRigidBody;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this);
        }
        else
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        previousHitNumbers = new List<int>();
        currentHitNumber = 0;
    }

    private void Start()
    {
        ResetBall();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GoToNextHole();
        }
    }

    public void GoToNextHole()
    {
        currentHole++;
        if (currentHole >= startingPositions.Count)
        {
            Debug.Log("Has completado todos los hoyos VAMOOO");
        }
        else
        {
            ResetBall();
        }
        previousHitNumbers.Add(currentHitNumber);
        currentHitNumber = 0;
        DisplayScore();
    }

    private void ResetBall()
    {
        ballRigidBody.transform.position = startingPositions[currentHole].position;
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
    }

    public void DisplayScore()
    {
        for (int i = 0; i < previousHitNumbers.Count; i++)
        {
            Debug.Log("HOLE " + (i+1) + " - HITS: " + previousHitNumbers[i]);
            scoreText.text += "HOLE " + (i + 1) + " - HITS: " + previousHitNumbers[i] + "<br>";
        }
    }
}
