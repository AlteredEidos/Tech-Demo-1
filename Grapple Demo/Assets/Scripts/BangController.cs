using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangController : MonoBehaviour
{
    Rigidbody2D rB;
    float speed = 25;
    float lifespan = 1;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        rB.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0);
    }
}
