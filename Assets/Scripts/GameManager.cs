using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameNetworkManager gameNetworkManager;
    public CameraManager cameraManager;
    public UIManager uiManager;
    public BulletManager bulletManager;
    public Leaderboard leaderboard;

    public bool gameOver;

    void Awake() {
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
        uiManager.Connected();
    }

    public void GameOver() {
        gameOver = true;
        gameNetworkManager.Disconnect();
    }

    private void QuitApplication()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
