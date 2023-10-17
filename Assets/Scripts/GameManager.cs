using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameNetworkManager gameNetworkManager;
    public CameraManager cameraManager;

    void Start() {
        Instance = this;
    }
    
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            QuitApplication();
        }
    }

    public void StartGame(){
        cameraManager.SetupCamera();
    }

    private void QuitApplication()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
