using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public Transform worldCursorTransform;

    void Start() {
    }

    Vector2 worldPosition;

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldCursorTransform.position = worldPosition;

        Cursor.visible = false;
        transform.position = mousePos;
    }
}
