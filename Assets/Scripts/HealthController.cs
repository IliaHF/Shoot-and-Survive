using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maximumHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return currentHealth / maximumHealth;
        }
    }

    public bool IsInvincible {get; set; }
    public UnityEvent OnHealthChanged;
    private Player player;

    void Awake() {
        player = gameObject.GetComponent<Player>();
        if(player.isLocalPlayer)
            OnChangeHealth();
    }

    void Start() {
        // if(!player.isLocalPlayer)
        //     CheckHealth();
    }

    void Update() {
        if(!player.isServer)
            CheckHealth();
    }

    private void GameOver() {
        if(player.isLocalPlayer)
            GameManager.Instance.GameOver();
    }

    public void TakeDamage(float damageAmount)
    {
        if(currentHealth == 0)
            return;

        if(IsInvincible)
            return;

        currentHealth -= damageAmount;
        OnChangeHealth();

        if(currentHealth <= 0)
            GameOver();
        else{
            // OnDamage
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if(currentHealth == maximumHealth)
            return;

        currentHealth += amountToAdd;
        OnChangeHealth();

        if(currentHealth > maximumHealth)
            currentHealth = maximumHealth;
    }

    public void OnChangeHealth() {
        Debug.Log("Player " + player.id + " took damage!");
        player.OnChangeHealth(currentHealth);
        OnHealthChanged.Invoke();
    }

    public void CheckHealth()
    {
        currentHealth = player.currentHealth;
        OnHealthChanged.Invoke();

        if(currentHealth <= 0)
            GameOver();
        else{
            // OnDamage
        }
    }
}
