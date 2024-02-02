using UnityEngine;

//Clase que controla que no se pueda coger más de un palo a la vez
public class HandsFullToggle : MonoBehaviour
{
    //Las manos tienen un pequeño collider que detecta si están sujetando algo
    [SerializeField] private SphereCollider handCollider;
    [SerializeField] private SphereCollider oppositeHandCollider;
    
    //Al ser un script único que tienen ambas manos, esta variable sirve para distinguirlas
    [SerializeField] private bool isRightHand;

    private void OnTriggerEnter(Collider other)
    {
        //Cuando se sujeta un palo se comunica al GameManager mediante una variable
        if (other.CompareTag("Club"))
        {
            if (isRightHand)
            {
                GameManager.sharedInstance.rightHandFull = true;
            }
            else
            {
                GameManager.sharedInstance.leftHandFull = true;
            }
            //Y se desactiva el collider de la otra mano
            oppositeHandCollider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Cuando se suelta un palo se comunica al GameManager mediante una variable
        if (other.CompareTag("Club"))
        {
            if (isRightHand)
            {
                GameManager.sharedInstance.rightHandFull = false;
            }
            else
            {
                GameManager.sharedInstance.leftHandFull = false;
            }
            //Y se activa el collider de la otra mano
            oppositeHandCollider.enabled = true;
        }
    }
}
