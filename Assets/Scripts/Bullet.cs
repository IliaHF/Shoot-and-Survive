using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int id;

    [SerializeField]
    private float damageAmount;
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player) {
            if (player.id == id)
                return;
            if(player.isServer){
                var HealthController = collision.gameObject.GetComponent<HealthController>();
                HealthController.TakeDamage(damageAmount);
            }
        }

        Destroy(gameObject);
    }

}

