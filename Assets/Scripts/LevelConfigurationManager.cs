using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelConfigurationManager : MonoBehaviour
{
    [SerializeField] private SnapTurnProviderBase playerSnapTurn;
    [SerializeField] private ContinuousTurnProviderBase playerContinuousTurn;
    [SerializeField] private TeleportationProvider playerTeleportation;
    [SerializeField] private ActivateTeleportationRay playerTpRayActivator;
    [SerializeField] private ContinuousMoveProviderBase playerContinuousMove;
    
    [SerializeField] private List<Transform> startingPositions;
    [SerializeField] private Rigidbody ballRigidBody;
    
    [SerializeField] private List<TextMeshProUGUI> scoreText;
    [SerializeField] private TextMeshProUGUI totalHitsText;
    [SerializeField] private TextMeshProUGUI recordHitsText;
    [SerializeField] private TextMeshProUGUI gameCompletedText;
    [SerializeField] private TextMeshProUGUI newRecordText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject exitButton;

    private void Awake()
    {
        playerSnapTurn.enabled = GameManager.sharedInstance.snapTurn;
        playerContinuousTurn.enabled = GameManager.sharedInstance.continuousTurn;
        playerTeleportation.enabled = GameManager.sharedInstance.teleportation;
        playerTpRayActivator.enabled = GameManager.sharedInstance.teleportation;
        playerContinuousMove.enabled = GameManager.sharedInstance.continuousMovement;

        GameManager.sharedInstance.startingPositions = startingPositions;
        GameManager.sharedInstance.ballRigidBody = ballRigidBody;

        GameManager.sharedInstance.scoreText = scoreText;
        GameManager.sharedInstance.totalHitsText = totalHitsText;
        GameManager.sharedInstance.recordHitsText = recordHitsText;
        GameManager.sharedInstance.gameCompletedText = gameCompletedText;
        GameManager.sharedInstance.newRecordText = newRecordText;
        GameManager.sharedInstance.restartButton = restartButton;
        GameManager.sharedInstance.exitButton = exitButton;
    }

    private void Start()
    {
        recordHitsText.text = "RECORD HITS: " + PlayerPrefs.GetInt("record");
    }

    public void ResetGame()
    {
        GameManager.sharedInstance.ResetGame();
    }

    public void ExitGame()
    {
        GameManager.sharedInstance.ExitGame();
    }
}
