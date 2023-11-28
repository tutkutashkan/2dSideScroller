using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemBullet : MonoBehaviour
{

    private new Rigidbody2D rigidbody;
    public float speed = 500.0f;
    public float maxLifetime = 4.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        rigidbody.velocity = transform.right * -speed;
        Destroy(gameObject, 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "enemy"){
            Destroy(gameObject);
        }
    }
}