using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager sharedInstance;
    
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private InputActionProperty menuButton;

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
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (menuButton.action.WasPressedThisFrame())
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            menuCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * menuHeadDistance;
        }
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
