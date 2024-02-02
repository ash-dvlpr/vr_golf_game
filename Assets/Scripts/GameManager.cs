using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que controla numerosos aspectos y variables del juego
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    
    //Esta propiedad mantiene su valor entre las diferentes estancias de GameManager
    [field: SerializeField] public bool CheatsEnabled { get; private set; } = false;
    
    //Variables de los hoyos
    private int currentHole;
    [field: Header("Holes variables")] public int currentHitNumber;
    private List<int> previousHitNumbers;
    private int totalHits;
    public List<Transform> startingPositions;
    
    //Variables de la UI
    [field: Header("UI variables")] public List<TextMeshProUGUI> scoreText;
    public TextMeshProUGUI totalHitsText;
    public TextMeshProUGUI recordHitsText;
    public GameObject extraInfoStuff;
    public Canvas newRecordText;
    [SerializeField] private Color completedHoleColor;
    [SerializeField] private Color currentHoleColor;
    [SerializeField] private Color incompletedHoleColor;
    [field: Space(20)]
    
    
    public Rigidbody ballRigidBody;

    //Variables de las manos
    public bool handsFull
    {
        get => leftHandFull || rightHandFull;
    }
    [field: Header("Hands variables")] public bool leftHandFull;
    public bool rightHandFull;

    //Variables de movimiento de jugador y cámara
    [field: Header("Movement variables")] [SerializeField] private TMP_Dropdown movementDropdown;
    public bool continuousMovement;
    public bool teleportation;

    [field: Header("Turn variables")] [SerializeField] private TMP_Dropdown turnDropdown;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoToNextHole();
        }
    }

    public void GoToNextHole()
    {
        //Si es el último hoyo se para a finalizar la partida
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
        //Primero se apaga el indicador de la bola (para los tramposos), se anuncia el final con unas trompetas y se cambia la música
        BallIndicator.sharedInstance.TurnOff();
        SoundManager.sharedInstance.trumpetsSound.Play();
        SoundManager.sharedInstance.gameMusic.Stop();
        SoundManager.sharedInstance.endGameMusic.Play();
        //Se pone el último marcador de golpes del color de los completados
        scoreText[scoreText.Count - 1].color = completedHoleColor;
        //Se actualiza el récord si este ha sido superado
        int record = PlayerPrefs.GetInt("record");
        if (totalHits < record || record == 0)
        {
            PlayerPrefs.SetInt("record", totalHits);
            recordHitsText.text = "RECORD HITS: " + totalHits;
            
        }
        //Se activan los elementos de la UI del final del juego
        extraInfoStuff.gameObject.SetActive(true);
        GameMenuManager.sharedInstance.ActivateCanvas();
        //Y finalmente si, de nuevo, se ha superado el récord, se activa la celebración correspondiente (tomaaa)
        if (totalHits < record || record == 0)
        {
            StartCoroutine(NewRecord());
        }
    }

    public void ResetGame()
    {
        //Se resetean las variables
        currentHole = 0;
        previousHitNumbers = new List<int>();
        currentHitNumber = 0;
        totalHits = 0;
        //Se reinicia la puntuación
        for (int i = 0; i < scoreText.Count; i++)
        {
            scoreText[i].text = (i+1) + "\t\t\t -\t\t00";
        }
        totalHitsText.text = "TOTAL HITS: 000";
        ChangeScoreColor();
        //Se oculta la UI de final de partida
        extraInfoStuff.gameObject.SetActive(false);
        //Se resetea la bola
        ResetBall();
        //Y se cambia la música
        SoundManager.sharedInstance.endGameMusic.Stop();
        SoundManager.sharedInstance.gameMusic.Play();
    }
    
    //Corutina que se activa al batir el récord
    private IEnumerator NewRecord()
    {
        newRecordText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        newRecordText.gameObject.SetActive(false);
    }

    public void UpdateCurrentHits()
    {
        //Al actualizarse en tiempo real, se comprueba si en este hoyo hay golpes
        //Si no es así se añade una nueva instancia de conteo
        if (currentHole >= previousHitNumbers.Count)
        {
            previousHitNumbers.Add(currentHitNumber);
        }
        //Si los había se añade el nuevo golpe
        else
        {
            previousHitNumbers[currentHole] += currentHitNumber;
        }
        currentHitNumber = 0;
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
        //Este método cambia el color de la puntuación comprobando en cada línea si se corresponde al hoyo actual,
        //si está por detrás o si está por delante
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
        //Habilita los tipos de movimientos elegidos en el menú de inicio
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
    {//Habilita el giro de cámara elegido en el menú de inicio
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
