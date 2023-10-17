using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private float speed = 4;
    public Vector3 lastPos;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public bool running;

    private void Start(){
        if(isLocalPlayer)
        {
            GameNetworkManager.Instance.SetPlayer();
        }
        animator = GetComponent<Animator>();
        lastPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RunAnimation());
    }

    private void HandleMovement()
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * speed;
            // transform.position = transform.position + movement * Time.deltaTime;
            rb.velocity = new Vector2(movement.x, movement.y);
            // rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        HandleMovement();




        var mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(mouse.x < playerScreenPoint.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator RunAnimation() {
        while(true) {
            yield return new WaitForSeconds(0.05f);

            if(transform.position != lastPos)
            {
                animator.SetBool("Running", true);
                running = true;
            }
            else
            {
                animator.SetBool("Running", false);
                running = false;
            }

            lastPos = transform.position;
        }
    }
}
