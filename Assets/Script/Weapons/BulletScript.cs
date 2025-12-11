using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float Bulletlifespan = 2;
    public int rotate;
    public float Damage = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - rotate);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Bulletlifespan)
        {
            Destroy(gameObject);
        }

        //Destroy(gameObject);




    }
    //void OnCollisionStay2D(Collision2D collision)
    //{
        
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Enemy enemy = GetComponent<Collider>().GetComponent<Enemy>();
    //        enemy.TakeDamage(Damage);
    //        Destroy(gameObject);
    //    }
    //}


}
