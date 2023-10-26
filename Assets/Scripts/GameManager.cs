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
        uiManager.GameOver();
        cameraManager.DisconnectCamera();
        gameNetworkManager.Disconnect();
    }

    private void QuitApplication()
    {
        Debug.Log("quitting");
        // if(!gameNetworkManager.localPlayer.GetComponent<Player>().isServer && !gameNetworkManager.localPlayer.GetComponent<Player>().isLocalPlayer)
        try{GameOver();}catch{}
        Application.Quit();
    }
}
