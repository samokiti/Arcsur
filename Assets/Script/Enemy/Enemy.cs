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
    private int chill = 0;
    public GameObject ExpOrb;
    public GameObject Hearth;
    public Transform weaponstats;
    //[Header("Debuff Settings")]
    //[SerializeField] private float slowDuration = 2f;

    //[SerializeField] private GameObject destroyEffect;
    void FixedUpdate()
    {
        float slow;
        if (chill > 0)
        {
            chill--;
            slow = 0.2f;
        }
        else
        {
            slow = 1f;

        }
        if (Controller.Instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        direction = (Controller.Instance.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * slow * movespeed, direction.y * slow * movespeed);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Controller.Instance.TakeDamage(1);
            Debug.Log("Enemy hit Player!");
            //Controller.Instance.ApplySlow(slowDuration);
            //Debug.Log("ApplySlow on Player.");
            Destroy(gameObject);
            //Instantiate(destroyEffect, transform.position, transform.rotation);
        }
        else if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            //Debug.Log("PlayerProjectile hit Enemy!");
            TakeDamage(Controller.Instance.skill2dmg);
        }
        else if (collision.gameObject.CompareTag("icebullet"))
        {
            //Debug.Log("PlayerProjectile hit Enemy!");
            chill += 15;
            TakeDamage(Controller.Instance.skill1dmg);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {

            Instantiate(ExpOrb, transform.position, transform.rotation);
            int rdh = Random.Range(1, 8);
            if (rdh == 7)
            {
                Instantiate(Hearth, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }


}
