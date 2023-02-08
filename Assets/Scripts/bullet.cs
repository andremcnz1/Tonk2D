using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject BulletExplosion;

    private GameObject parent;

    public bullet(GameObject parent)
    {
        this.parent = parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != parent)
        {
            GameObject effect = Instantiate(BulletExplosion, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        }
    }
}
