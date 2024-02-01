using UnityEngine;

//Clase que activa el paso al siguiente hoyo al detectar la bola
public class HoleTrigger : MonoBehaviour
{
    private string targetTag = "Ball";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            GameManager.sharedInstance.GoToNextHole();
        }
    }
}
