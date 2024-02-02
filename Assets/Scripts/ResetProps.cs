using UnityEngine;

//Clase que resetea la bola y los palos al tocar un plano de reset (como el suelo de césped)
public class ResetProps : MonoBehaviour
{
    [SerializeField] private Transform resetArea;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ResetPlane"))
        {
            if (CompareTag("Ball"))
            {
                GameManager.sharedInstance.ResetBall();
            }

            if (CompareTag("Club") && !GameManager.sharedInstance.handsFull)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                transform.rotation = resetArea.rotation;
                transform.position = resetArea.position;
            }
        }
    }
}
