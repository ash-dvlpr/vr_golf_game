using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//Clase que propulsa la bola al ser golpeada por la cabeza del palo
public class LaunchBall : MonoBehaviour
{
    [SerializeField] private string targetTag;

    //Variables de velocidad
    private static int BUFFER_LENGTH = 3;
    private readonly Queue<Vector3> velocityBuffer = new();

    //Variables del palo
    private Vector3 previousPosition;
    private Collider clubCollider;
    
    //Sonidos
    [SerializeField] private AudioSource slowHitSound;
    [SerializeField] private AudioSource hardHitSound;

    [SerializeField] private Rigidbody ballRB;

    private void Awake()
    {
        clubCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        velocityBuffer.Clear();
        for(int i = 0; i < BUFFER_LENGTH; i++) {
            velocityBuffer.Enqueue(Vector3.zero);
        }
    }

    private void Update()
    {
        //Calcula la velocidad en este frame
        var velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        //Almacena la velocidad
        velocityBuffer.Enqueue(velocity);
        velocityBuffer.Dequeue(); //Elimina la velocidad más antigua
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && ballRB.velocity == Vector3.zero)
        {
            //Calcula la velocidad compuesta media
            var _compoundVelocity = velocityBuffer.ToList().Aggregate((a, b) => a + b);
            _compoundVelocity /= velocityBuffer.Count;

            //Se incrementa el número de golpes
            GameManager.sharedInstance.currentHitNumber++;
            
            Vector3 collisionPos = clubCollider.ClosestPoint(other.transform.position);
            Vector3 collisionNormal = other.transform.position - collisionPos;
            Vector3 projectedVelocity = Vector3.Project(_compoundVelocity, collisionNormal);
            
            //Aplica la velocidad
            Rigidbody rBall = other.attachedRigidbody;
            rBall.velocity = projectedVelocity;

            //Reproduce un audio dependiendo de la fuerza del golpe
            if (projectedVelocity.sqrMagnitude < 2f)
            {
                slowHitSound.Play();
            }
            else
            {
                hardHitSound.Play();
            }
            
            //Se oculta el indicador de bola
            BallIndicator.sharedInstance.TurnOff();
            
            //Se actualiza la puntuación
            GameManager.sharedInstance.UpdateCurrentHits();
            GameManager.sharedInstance.DisplayScore();
        }
    }
}
