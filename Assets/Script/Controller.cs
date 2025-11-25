using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator; 
    [SerializeField] private float movespeed;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;
    private void Awake()
    {
        if (instance != null && instance != this) 
        { Destroy(this); } 
        else 
        { instance = this; }
    }

    private void Start()
    {
        playerHealth = playerMaxHealth;

    }
    void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3 (InputX, InputY).normalized;

        animator.SetFloat("moveX", InputX);
        animator.SetFloat("moveY", InputY);

        if (playerMoveDirection == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerMoveDirection.x * movespeed, playerMoveDirection.y * movespeed);
    }
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }
}
