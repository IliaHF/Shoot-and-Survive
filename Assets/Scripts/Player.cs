using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private float speed = 4;
    private Vector3 lastPos;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start(){
        if(isLocalPlayer)
        {
            GameNetworkManager.Instance.SetPlayer();
        }
        animator = GetComponent<Animator>();
        lastPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void HandleMovement()
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * Time.deltaTime * speed;
            transform.position = transform.position + movement;
        }
    }

    void Update()
    {
        HandleMovement();

        
        if(transform.position != lastPos)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        lastPos = transform.position;


        var mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(mouse.x < playerScreenPoint.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }
}
