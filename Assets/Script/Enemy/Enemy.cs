using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] private float movespeed;
    [SerializeField] private GameObject destroyEffect;
    void FixedUpdate()
    {
        if (Controller.instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        direction = (Controller.instance.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
    }
    void OnCollisionStay2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }
}
