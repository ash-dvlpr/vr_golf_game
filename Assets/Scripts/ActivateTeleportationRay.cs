using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

//Clase que maneja la activación del rayo de teletransporte
public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;
    
    public InputActionProperty leftSelect;
    public InputActionProperty rightSelect;

    public XRRayInteractor leftGrabRay;
    public XRRayInteractor rightGrabRay;

    private void Update()
    {
        bool isLeftRayHovering = leftGrabRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        bool isRightRayHovering = rightGrabRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);
        
        //El rayo de teletransporte se activará si se pulsa el gatillo de abajo y no está apuntando a un elemento de la UI
        leftTeleportation.SetActive(!isLeftRayHovering && leftSelect.action.ReadValue<float>()==0 && leftActivate.action.ReadValue<float>()>0.1f);
        rightTeleportation.SetActive(!isRightRayHovering && rightSelect.action.ReadValue<float>()==0 && rightActivate.action.ReadValue<float>()>0.1f);
    }
}
