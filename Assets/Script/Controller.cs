using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float movespeed;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;
    public float playerMaxEXP;
    public float playerEXP;

    public int exp;
    public int currentlevel;
    public int maxlevel;
    public List<int> playerlevel;

    [Header("Slow Debuff")]
    private float originalMoveSpeed;
    private float slowDurationTimer;
    [SerializeField] private float slowPercentage = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }
        originalMoveSpeed = movespeed;
    }

    void Start()
    {
        for (int i = playerlevel.Count; i < maxlevel; i++)
        {
            playerlevel.Add(Mathf.CeilToInt(playerlevel[playerlevel.Count - 1] * 1.1f + 10));
        }
        playerHealth = playerMaxHealth;
        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.UpdateEXPSlider();

    }
    void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(InputX, InputY).normalized;

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
        if (slowDurationTimer > 0)
        {
            slowDurationTimer -= Time.deltaTime;

            if (slowDurationTimer <= 0)
            {
                movespeed = originalMoveSpeed;
                Debug.Log("Slow Debuff Ended. Speed Restored.");
            }
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerMoveDirection.x * movespeed, playerMoveDirection.y * movespeed);
    }
    public void ApplySlow(float duration)
    {
        movespeed = originalMoveSpeed * slowPercentage;

        slowDurationTimer = duration;
        Debug.Log($"Player is SLOWED for {duration} seconds.");
    }
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        UIController.Instance.UpdateHealthSlider();
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void getexp(int exptoget)
    {
        exp += exptoget;
        UIController.Instance.UpdateEXPSlider();
    }
}
