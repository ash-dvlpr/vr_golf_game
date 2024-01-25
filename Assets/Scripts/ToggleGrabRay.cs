using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleGrabRay : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor leftDirectGrab;
    [SerializeField] private XRDirectInteractor rightDirectGrab;

    [SerializeField] private GameObject leftGrabRay;
    [SerializeField] private GameObject rightGrabRay;

    private void Update()
    {
        //Si la mano contraria está ocupada no se activa
        leftGrabRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0 && !GameManager.sharedInstance.rightHandFull);
        rightGrabRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0 && !GameManager.sharedInstance.leftHandFull);
    }
}
