using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public GameObject AiPlayer;
    public float speed;

    private float distance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, AiPlayer.transform.position);
        Vector2 direction = AiPlayer.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, AiPlayer.transform.position, speed * Time.deltaTime);
    }
}
