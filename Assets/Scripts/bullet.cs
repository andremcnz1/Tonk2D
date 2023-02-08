using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject BulletExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.tag != info.Tag)
        // {
            GameObject effect = Instantiate(BulletExplosion, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        // }
    }
}
