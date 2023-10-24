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

    public UnityEvent OnDied;


    public UnityEvent OnDamage;


    public UnityEvent OnHealthChanged;


    private Player player;
    void Start() {
        player = gameObject.GetComponent<Player>();
    }
    private void GameOver() {
        if(player.isLocalPlayer) {
            GameManager.Instance.GameOver();
        }
    }

    
    
    public void TakeDamage(float damageAmount)
    {

        if(currentHealth == 0)
        {
            return;
        }  

        if(IsInvincible)
        {
            return;
        }

        currentHealth -= damageAmount;

        OnHealthChanged.Invoke();



        if(currentHealth < 0)
        {
            currentHealth = 0;
        }


        if(currentHealth == 0)
        {
            OnDied.Invoke();
            GameOver();
        }
        else
        {
            OnDamage.Invoke();

        }
    }


    public void AddHealth(float amountToAdd)
    {
        if(currentHealth == maximumHealth)
        {


            return;
        }



        currentHealth += amountToAdd;

        OnHealthChanged.Invoke();

        if(currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }


}
