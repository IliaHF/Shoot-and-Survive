using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class mainGun : MonoBehaviour
{
    
    public Transform shootPoint;
    public float bulletSpeed;
    public GameObject bulltet;
    public float bulletLifeTime = 1.0f;

    [SerializeField]
    private Player player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
        
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulltet, shootPoint.position, Quaternion.Euler(180, 180, shootPoint.eulerAngles.z));

        Rigidbody2D bulltetRb = newBullet.GetComponent<Rigidbody2D>();
        newBullet.GetComponent<Bullet>().id = player.id;

        bulltetRb.AddForce(-newBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);

        Destroy(newBullet, bulletLifeTime);

    }
}
