using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelConfigurationManager : MonoBehaviour
{
    [SerializeField] private SnapTurnProviderBase playerSnapTurn;
    [SerializeField] private ContinuousTurnProviderBase playerContinuousTurn;
    [SerializeField] private TeleportationProvider playerTeleportation;
    [SerializeField] private ActivateTeleportationRay playerTPRayActivator;
    [SerializeField] private ContinuousMoveProviderBase playerContinuousMove;

    private void Awake()
    {
        playerSnapTurn.enabled = GameManager.sharedInstance.snapTurn;
        playerContinuousTurn.enabled = GameManager.sharedInstance.continuousTurn;
        playerTeleportation.enabled = GameManager.sharedInstance.teleportation;
        playerTPRayActivator.enabled = GameManager.sharedInstance.teleportation;
        playerContinuousMove.enabled = GameManager.sharedInstance.continuousMovement;
    }
}
