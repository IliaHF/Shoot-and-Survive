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

    public TextMeshProUGUI[] playersScore;
    public TextMeshProUGUI[] playersName;
    public string playerName;
    public TMP_InputField playerNameInput;

    // Start is called before the first frame update
    void Start()
    {
        playerName = "User" + Random.Range(100, 999).ToString();
        playerNameInput.text = playerName;
        MenuIP.text = GameNetworkManager.Instance.defaultIP;
        EnterMainMenu();
    }

    public void ChangeName(string name) {
        playerName = name;
    }

    public void EnterMainMenu() {
        GameManager.Instance.gameOver = false;
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
