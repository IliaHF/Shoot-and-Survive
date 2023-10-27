using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public Transform worldCursorTransform;
    public bool isEnabled;

    void Start() {
    }

    Vector2 worldPosition;

    void Update()
    {
        if(!isEnabled)
            return;

        Vector2 mousePos = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldCursorTransform.position = worldPosition;

        Cursor.visible = false;
        transform.position = mousePos;
    }

    void OnEnable(){
        isEnabled = true;
    }

    void OnDisable(){
        isEnabled = false;
        Cursor.visible = true;
    }
}
