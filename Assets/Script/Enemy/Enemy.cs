using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] private float movespeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private int exptogive;
    [SerializeField] private float pushtime;
    public GameObject ExpOrb;
    public GameObject Hearth;
    private float pushcounter;
    //[Header("Debuff Settings")]
    //[SerializeField] private float slowDuration = 2f;

    //[SerializeField] private GameObject destroyEffect;
    void FixedUpdate()
    {
        if (Controller.Instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        direction = (Controller.Instance.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Controller.Instance.TakeDamage(1);
            Debug.Log("Enemy hit Player!");
            //Controller.Instance.ApplySlow(slowDuration);
            Debug.Log("ApplySlow on Player.");
            Destroy(gameObject);
            //Instantiate(destroyEffect, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Debug.Log("PlayerProjectile hit Enemy!");
            TakeDamage(1);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        pushcounter = pushtime;
        if (health <= 0)
        {
          
            Instantiate(ExpOrb, transform.position, transform.rotation);
            Instantiate(Hearth, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


}
