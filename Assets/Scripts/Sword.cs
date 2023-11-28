using UnityEngine;

public class Sword : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed = 20.0f;
    public float maxLifetime = 5.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Project(float direction)
    {
        rigidbody.velocity = transform.right * speed *direction;
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
