using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform ShootingPosition;
    public GameObject R35Bullet;

    public float BulletForce = 3.0f;
    void Update()
    {
        //switch (info.playerType)
        //{
        //    case PlayerType.Local:
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
        //        break;
        //}
        
    }
    void Shoot()
    {
        GameObject Bullet = Instantiate(R35Bullet, ShootingPosition.position, ShootingPosition.rotation);
        // Bullet.GetComponent<bullet>().info = info;
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(ShootingPosition.up * BulletForce, ForceMode2D.Impulse);
    }
}
