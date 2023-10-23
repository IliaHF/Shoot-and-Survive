using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameOverMenu;
    public GameObject GameUI;
    public TMP_InputField MenuIP;
    public GameObject ConnectingMenu;
    public GameObject MainConnectingMenu;
    public GameObject ServerMenu;

    // Start is called before the first frame update
    void Start()
    {
        EnterMainMenu();
        MenuIP.text = GameNetworkManager.Instance.defaultIP;
    }

    public void EnterMainMenu() {
        GameOverMenu.SetActive(false);
        GameUI.SetActive(false);
        MainMenu.SetActive(true);
        CancelConnection();
    }

    public void Connecting() {
        ConnectingMenu.SetActive(true);
        MainConnectingMenu.SetActive(false);
    }
    public void CancelConnection() {
        ConnectingMenu.SetActive(false);
        MainConnectingMenu.SetActive(true);
    }
    public void Connected() {
        EnterTheGame();
        CancelConnection();
    }

    public void EnterTheGame() {
        MainMenu.SetActive(false);
        GameUI.SetActive(true);
    }

    public void GameOver() {
        GameUI.SetActive(false);
        GameOverMenu.SetActive(true);
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
