using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class GameNetworkManager : NetworkManager
{
    public static GameNetworkManager Instance;
    public GameObject localPlayer;
    public string defaultIP;

    public bool connecting;
    public int NewPlayerID;

    public TextMeshProUGUI IPText;

    private void Awake()
    {
        Instance = this;
        networkAddress = defaultIP;
    }

    public void ChangeIP(string ip) {
        networkAddress = ip;
    }

    public void Disconnect() {
        StopClient();
        // StopServer();
    }

    public void HostServer()
    {
        // StartServer();
        StartHost();
        GameManager.Instance.uiManager.ServerMenu.SetActive(true);
        // IPText.text = NetworkManager.singleton.networkAddress;
    }

    public void ConnectToServer()
    {
        if (networkAddress.Trim().Length == 0)
            networkAddress = "localhost";
        StartClient();
    }

    public void SetPlayer() {
        StartCoroutine(SetPlayerCoroutine());
    }

    IEnumerator SetPlayerCoroutine() {
        Debug.Log("SetPlayerCoroutine");
        yield return new WaitWhile(() => NetworkClient.localPlayer.gameObject == null);

        localPlayer = NetworkClient.localPlayer.gameObject;
        GameManager.Instance.StartGame();
    }


    public void HostAndConnectToServer()
    {
    }

    public void StopHostingServer() {
        StopServer();
        GameManager.Instance.uiManager.ServerMenu.SetActive(false);
        GameManager.Instance.uiManager.EnterMainMenu();
        GameManager.Instance.cameraManager.DisconnectCamera();
        GameManager.Instance.uiManager.CancelConnection();
    }

    public override void OnStopClient ()
    {
        if(GameManager.Instance.gameOver)
            GameManager.Instance.uiManager.GameOver();
        else
            GameManager.Instance.uiManager.EnterMainMenu();
        GameManager.Instance.cameraManager.DisconnectCamera();
        GameManager.Instance.uiManager.CancelConnection();
    }
    public override void OnClientDisconnect ()
    {
        GameManager.Instance.uiManager.CancelConnection();
    }
}
