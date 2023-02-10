using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class bullet : MonoBehaviour
{
    public GameObject BulletExplosion;

    [SerializeField] private float ExplostionTime = 0.3f;
    [SerializeField] private float MaxLifeSpan = 5.0f;

    private float LifeSpan = 0.0f;

    private GameObject parent;

    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }

    public GameObject GetParent()
    {
        return parent;
    }

    private void Update()
    {
        if(LifeSpan >= MaxLifeSpan)
        {
            Destroy(gameObject);
        }

        LifeSpan += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != parent)
        {
            GameObject effect = Instantiate(BulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, ExplostionTime);
        }
    }
}
