using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Player : NetworkBehaviour
{
    private float speed = 4;
    public Vector3 lastPos;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer gunSpriteRenderer;
    private Rigidbody2D rb;
    [SerializeField]
    private mainGun _mainGun;

    [SerializeField]
    private SpriteRenderer PlayerSprite;
    [SerializeField]
    private SpriteRenderer GunSprite;
    [SerializeField]
    private Collider2D playerCollider;
    [SerializeField]
    private GameObject HealthBar;
    
    [SyncVar]
    public bool ServerPlayer;
    public TextMeshProUGUI playerName;
    

    private void Start(){
        if(isLocalPlayer)
        {
            GameNetworkManager.Instance.SetPlayer();
            if(isServer) {
                ServerPlayer = true;
                GameManager.Instance.cameraManager.ServerCamera();
            }
            else {
                transform.position = new Vector2(Random.Range(-22, 22), Random.Range(-22, 22));
            }
        }
        if(ServerPlayer) {
            PlayerSprite.enabled = false;
            GunSprite.enabled = false;
            playerCollider.enabled = false;
            HealthBar.SetActive(false);
        }
        else {
            ClientJoined();
        }
        animator = GetComponent<Animator>();
        lastPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RunAnimation());
    }

    [Client]
    private void ClientJoined() {
        AddPlayer(GameManager.Instance.uiManager.playerName);
    }
    
    [SyncVar]
    public int id;
    [SyncVar]
    public string name;
    [Command]
    private void AddPlayer(string _name) {
        Debug.Log("AddPlayer");
        GameNetworkManager.Instance.NewPlayerID++;
        id = GameNetworkManager.Instance.NewPlayerID;
        name = _name;
        GameManager.Instance.leaderboard.AddPlayer(id, name);
    }

    void OnDestroy() {
        if(isServer) {
            DisconnectPlayer();
        }
    }

    private void DisconnectPlayer() {
        Debug.Log("Player: player disconnected");
        GameManager.Instance.leaderboard.RemovePlayer(id);
        AddPointTo(lastShotID);
    }

    public int lastShotID;

    private void AddPointTo(int _id) {
        GameManager.Instance.leaderboard.AddScore(_id);
    }

    [SyncVar]
    public float currentHealth = 100;
    public void OnChangeHealth(float health) {
        currentHealth = health;
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * speed;
        // transform.position = transform.position + movement * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, movement.y);
        // rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }

    void Update()
    {
        if(isLocalPlayer)
        {
            HandleMovement();
            FlipPlayer();
            GunRotation();
            if(!isServer)
                SyncValues();
            else
                SendChangesToPlayer(flip, angle);

            spriteRenderer.flipX = flip;
            gunSpriteRenderer.flipY = flip;
            gunRotation.rotation = Quaternion.Euler(0,0, angle);
        }
        else {
            spriteRenderer.flipX = serverFlip;
            gunSpriteRenderer.flipY = serverFlip;
            gunRotation.rotation = Quaternion.Euler(0,0, serverAngle);
        }
        playerName.text = name;
        
    }
    private bool flip;
    private void FlipPlayer(){

        var mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(mouse.x < playerScreenPoint.x) {
            flip = true;
        } else {
            flip = false;
        }
    }

    private Vector3 mouse_pos, object_pos;
    private float angle;
    [SerializeField]
    private Transform gunRotation;
    private void GunRotation() {
        
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(gunRotation.position);

        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;

        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
    }

    [Client]
    private void SyncValues()
    {
        // Debug.Log(angle);
        SendChangesToPlayer(flip, angle);
    }
    
    [SyncVar]
    private bool serverFlip;
    [SyncVar]
    private float serverAngle;
    [Command(requiresAuthority = false)]
    private void SendChangesToPlayer(bool _flip, float _angle) {
        serverFlip = _flip;
        // Debug.Log(_angle + " " + serverAngle);
        serverAngle = _angle;
    }


    IEnumerator RunAnimation() {
        while(true) {
            yield return new WaitForSeconds(0.05f);

            if(transform.position != lastPos)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }

            lastPos = transform.position;
        }
    }


    public void Shoot() {
        if(isServer)
            NetworkShoot();
        else
            ClientNetworkShoot();
    }

    
    [ClientRpc]
    private void NetworkShoot() {
        Debug.Log("NetworkShoot!");
        _mainGun.Shoot();
    }
    
    [Command]
    private void ClientNetworkShoot() {
        Debug.Log("ClientNetworkShoot!");
        NetworkShoot();
    }

}
