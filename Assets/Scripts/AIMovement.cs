using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject R35Bullet;
    [SerializeField] private GameObject Turret;
    [SerializeField] private float speed;
    [SerializeField] private float distanceBetween;
    [SerializeField] private float BulletSpeed = 4.0f;

    private float distance;
    void Start()
    {

    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 90.0f));

        }
      


    }
}
