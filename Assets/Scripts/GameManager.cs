using System.Collections;
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
    private int totalHits;
    
    public List<TextMeshProUGUI> scoreText;
    public TextMeshProUGUI totalHitsText;
    public TextMeshProUGUI recordHitsText;
    public TextMeshProUGUI gameCompletedText;
    public TextMeshProUGUI newRecordText;
    [SerializeField] private Color completedHoleColor;
    [SerializeField] private Color currentHoleColor;
    [SerializeField] private Color incompletedHoleColor;
    public GameObject restartButton;
    public GameObject exitButton;

    public List<Transform> startingPositions;

    public Rigidbody ballRigidBody;

    public bool handsFull
    {
        get => leftHandFull || rightHandFull;
    }
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
        if(sharedInstance != null && sharedInstance != this)
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
        totalHits = 0;
        currentHole = 0;
    }

    private void Update()
    {
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GoToNextHole();
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoToNextHole();
        }
    }

    public void GoToNextHole()
    {
        currentHole++;
        if (currentHole >= startingPositions.Count)
        {
            FinishGame();
        }
        else
        {
            SoundManager.sharedInstance.clappingSound.Play();
            ResetBall();
            UpdateCurrentHits();
            currentHitNumber = 0;
            DisplayScore();
        }
    }

    public void FinishGame()
    {
        BallIndicator.sharedInstance.TurnOff();
        SoundManager.sharedInstance.gameMusic.Stop();
        SoundManager.sharedInstance.endGameMusic.Play();
        scoreText[scoreText.Count - 1].color = completedHoleColor;
        int record = PlayerPrefs.GetInt("record");
        if (totalHits < record || record == 0)
        {
            PlayerPrefs.SetInt("record", totalHits);
            recordHitsText.text = "RECORD HITS: " + totalHits;
        }
        gameCompletedText.gameObject.SetActive(true);
        restartButton.SetActive(true);
        exitButton.SetActive(true);
        GameMenuManager.sharedInstance.ActivateCanvas();
        StartCoroutine(NewRecord());
    }

    public void ResetGame()
    {
        currentHole = 0;
        previousHitNumbers = new List<int>();
        currentHitNumber = 0;
        totalHits = 0;
        for (int i = 0; i < scoreText.Count; i++)
        {
            scoreText[i].text = (i+1) + "\t\t\t -\t\t00";
        }
        totalHitsText.text = "TOTAL HITS: 000";
        ChangeScoreColor();
        gameCompletedText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        ResetBall();
        SoundManager.sharedInstance.endGameMusic.Stop();
        SoundManager.sharedInstance.gameMusic.Play();
    }

    private IEnumerator NewRecord()
    {
        newRecordText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        newRecordText.gameObject.SetActive(false);
    }

    public void UpdateCurrentHits()
    {
        if (currentHole >= previousHitNumbers.Count)
        {
            previousHitNumbers.Add(currentHitNumber);
        }
        else
        {
            previousHitNumbers[currentHole] += currentHitNumber;
        }
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
        totalHits = 0;
        for (int i = 0; i < previousHitNumbers.Count; i++)
        {
            Debug.Log("HOLE " + (i+1) + " - HITS: " + previousHitNumbers[i]);
            totalHits += previousHitNumbers[i];
            scoreText[i].text = (i+1) + "\t\t\t -\t\t" + previousHitNumbers[i];
        }
        totalHitsText.text = "TOTAL HITS: " + totalHits;
        ChangeScoreColor();
    }

    private void ChangeScoreColor()
    {
        for (int i = 0; i < scoreText.Count; i++)
        {
            if (i < currentHole)
            {
                scoreText[i].color = completedHoleColor;
            }

            if (i == currentHole)
            {
                scoreText[i].color = currentHoleColor;
            }

            if (i > currentHole)
            {
                scoreText[i].color = incompletedHoleColor;
            }
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
