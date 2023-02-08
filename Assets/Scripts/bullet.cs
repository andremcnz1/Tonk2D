using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class bullet : MonoBehaviour
{
    public GameObject BulletExplosion;

    public GameObject parent;

    public void SetParent(GameObject parent)
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
