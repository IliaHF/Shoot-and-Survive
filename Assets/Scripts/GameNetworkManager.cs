using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetworkManager : NetworkManager
{
    public static GameNetworkManager Instance;
    public GameObject localPlayer;
    public string defaultIP;

    public bool connecting;

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
        StopServer();
        GameManager.Instance.uiManager.ServerMenu.SetActive(false);
    }

    public void HostServer()
    {
        StartHost();
        GameManager.Instance.uiManager.ServerMenu.SetActive(true);
    }

    public void ConnectToServer()
    {
        StartClient();
    }

    public void SetPlayer() {
        StartCoroutine(SetPlayerCoroutine());
    }

    IEnumerator SetPlayerCoroutine() {
        Debug.Log("SetPlayerCoroutine");
        yield return new WaitWhile(() => NetworkClient.localPlayer.gameObject == null);
        Debug.Log(NetworkClient.localPlayer.gameObject);

        localPlayer = NetworkClient.localPlayer.gameObject;
        GameManager.Instance.StartGame();
    }


    public void HostAndConnectToServer()
    {
        
    }

    public override void OnStopClient ()
    {
        GameManager.Instance.uiManager.CancelConnection();
    }
}
