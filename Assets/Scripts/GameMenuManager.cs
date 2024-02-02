using UnityEngine;
using UnityEngine.InputSystem;

//Clase que controla la orientación y posición del menú de puntuaciones,
//haciendo que se ponga frente al jugador cada vez que se active
public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager sharedInstance;
    
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private InputActionProperty menuButton;
#if UNITY_EDITOR || PLATFORM_ANDROID
    [SerializeField] private InputActionProperty cheatRecordButton;
#endif
    [SerializeField] private Transform head;
    [SerializeField] private float menuHeadDistance;

    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(this);
        }
        else
        {
            sharedInstance = this;
        }
    }

    private void Update()
    {
        if (menuButton.action.WasPressedThisFrame())
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            menuCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * menuHeadDistance;
        }

#if UNITY_EDITOR || PLATFORM_ANDROID
        if(cheatRecordButton.action.WasPressedThisFrame() && GameManager.sharedInstance.CheatsEnabled) {
            PlayerPrefs.SetInt("record", int.MaxValue);
            GameManager.sharedInstance.GoToNextHole();
        }
#endif
        menuCanvas.transform.LookAt(new Vector3(head.position.x, menuCanvas.transform.position.y, head.position.z));
        //Para que no aparezca el menú en modo espejo
        menuCanvas.transform.forward *= -1;
    }

    public void ActivateCanvas()
    {
        menuCanvas.SetActive(true);
        menuCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * menuHeadDistance;
    }
}
