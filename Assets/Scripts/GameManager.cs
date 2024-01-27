using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    
    private int currentHole;
    public int currentHitNumber;
    private List<int> previousHitNumbers;
    public TextMeshPro scoreText;

    [SerializeField] private List<Transform> startingPositions;

    [SerializeField] private Rigidbody ballRigidBody;

    public bool leftHandFull;
    public bool rightHandFull;

    [SerializeField] private TMP_Dropdown movementDropdown;
    public bool continuousMovement;
    public bool teleportation;

    [SerializeField] private TMP_Dropdown turnDropdown;
    public bool continuousTurn;
    public bool snapTurn;

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

    public void ResetBall()
    {
        ballRigidBody.transform.position = startingPositions[currentHole].position;
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        BallIndicator.sharedInstance.TurnOn();
    }

    public void DisplayScore()
    {
        for (int i = 0; i < previousHitNumbers.Count; i++)
        {
            Debug.Log("HOLE " + (i+1) + " - HITS: " + previousHitNumbers[i]);
            scoreText.text += "HOLE " + (i + 1) + " - HITS: " + previousHitNumbers[i] + "<br>";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeMovement()
    {
        switch (movementDropdown.value)
        {
            case 0:
            {
                continuousMovement = true;
                teleportation = true;
            } break;
            case 1:
            {
                continuousMovement = true;
                teleportation = false;
            } break;
            case 2:
            {
                continuousMovement = false;
                teleportation = true;
            } break;
        }
    }

    public void ChangeTrun()
    {
        switch (turnDropdown.value)
        {
            case 0:
            {
                snapTurn = true;
                continuousTurn = false;
            } break;
            case 1:
            {
                snapTurn = false;
                continuousTurn = true;
            } break;
        }
    }
}
