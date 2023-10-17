using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager Instance;
    NetworkManager networkManager;
    public GameObject localPlayer;

    private void Start()
    {
        networkManager = gameObject.GetComponent<NetworkManager>();
        Instance = this;
    }

    public void HostServer()
    {
        networkManager.StartHost();
    }

    public void ConnectToServer()
    {
        networkManager.StartClient();
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
}
