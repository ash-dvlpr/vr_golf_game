using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Clase que asigna numerosas variables de la MainScene al GameManager
public class LevelConfigurationManager : MonoBehaviour
{
    //Movimiento y cámara del jugador
    [SerializeField] private SnapTurnProviderBase playerSnapTurn;
    [SerializeField] private ContinuousTurnProviderBase playerContinuousTurn;
    [SerializeField] private TeleportationProvider playerTeleportation;
    [SerializeField] private ActivateTeleportationRay playerTpRayActivator;
    [SerializeField] private ContinuousMoveProviderBase playerContinuousMove;
    
    //Variables del campo de golf
    [SerializeField] private List<Transform> startingPositions;
    [SerializeField] private Rigidbody ballRigidBody;
    
    //Variables de la UI
    [SerializeField] private List<TextMeshProUGUI> scoreText;
    [SerializeField] private TextMeshProUGUI totalHitsText;
    [SerializeField] private TextMeshProUGUI recordHitsText;
    [SerializeField] private GameObject extraInfoStuff;
    [SerializeField] private Canvas newRecordText;

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
        GameManager.sharedInstance.extraInfoStuff = extraInfoStuff;
        GameManager.sharedInstance.newRecordText = newRecordText;
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
