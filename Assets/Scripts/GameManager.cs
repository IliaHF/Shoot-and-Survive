using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            QuitApplication();
        }
    }

    private void QuitApplication()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
