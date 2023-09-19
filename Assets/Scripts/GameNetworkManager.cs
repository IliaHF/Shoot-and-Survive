using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetworkManager : MonoBehaviour
{
    NetworkManager networkManager;

    private void Start()
    {
        networkManager = gameObject.GetComponent<NetworkManager>();
    }

    public void HostServer()
    {
        networkManager.StartHost();
    }

    public void ConnectToServer()
    {
        networkManager.StartClient();
    }

    public void HostAndConnectToServer()
    {
        
    }
}
