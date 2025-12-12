using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer; 

    [SerializeField] private float movespeed;
    public float extraspeed = 1f;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;
    public float playerMaxEXP;
    public float playerEXP;
    public float VelocityX = 0;
    public float VelocityY = 0;
    public float friction = 0.95f;
    public int exp;
    public int currentlevel;
    public int maxlevel;
    public List<int> playerlevel;
    private float exspeed;
    public Weapon activeweapon;

    [Header("Skills Stats")]
    public float skill1dmg = 2f;
    public float skill2dmg = 5f;
    public float skill1cd = 2f;
    public float skill2cd = 0.3f;

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
        if (playerlevel.Count == 0) playerlevel.Add(10);
        for (int i = playerlevel.Count; i < maxlevel; i++)
        {
            playerlevel.Add(Mathf.CeilToInt(playerlevel[playerlevel.Count - 1] * 1.1f + 10));
        }
        playerHealth = playerMaxHealth;
        playerEXP = exp;
        playerMaxEXP = playerlevel[currentlevel];

        if (UIController.Instance != null)
        {
            UIController.Instance.UpdateHealthSlider();
            UIController.Instance.UpdateEXPSlider();
        }
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        if (InputX != 0 || InputY != 0)
        {
            if (animator != null) animator.SetBool("isRunning", true);
        }
        else
        {
            if (animator != null) animator.SetBool("isRunning", false);
        }
        if (InputX > 0)
        {
            if (spriteRenderer != null) spriteRenderer.flipX = false;
        }
        else if (InputX < 0)
        {
            if (spriteRenderer != null) spriteRenderer.flipX = true;
        }
        VelocityX *= friction;
        if (VelocityX > -0.05f && VelocityX < 0.05f) VelocityX = 0;
        if (VelocityX > -1f && VelocityX < 1f) VelocityX += InputX * 0.06f;

        VelocityY *= friction;
        if (VelocityY > -0.05f && VelocityY < 0.05f) VelocityY = 0;
        if (VelocityY > -1f && VelocityY < 1f) VelocityY += InputY * 0.06f;

        playerMoveDirection = new Vector3(VelocityX, VelocityY).normalized;
        if (extraspeed > 0f)
        {
            extraspeed -= 1f;
            exspeed = movespeed * 0.5f;
        }
        else
        {
            extraspeed = 0f;
            exspeed = 0;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(VelocityX * (movespeed + exspeed), VelocityY * (movespeed + exspeed));
    }

    public void ApplySlow(float duration)
    {
        movespeed = originalMoveSpeed * slowPercentage;
        slowDurationTimer = duration;
    }

    public void TakeDamage(float damage)
    {
        if(playerHealth - damage > playerMaxHealth)
        {
            damage = 0;
        }
        if (damage < 0)
        {
            extraspeed =  50f;
        }
        playerHealth -= damage;
        UIController.Instance.UpdateHealthSlider();

        if (playerHealth <= 0)
        {
            if (GameManager.Instance != null) GameManager.Instance.TriggerGameOver();
        }
    }

    public void getexp(int exptoget)
    {
        exp += exptoget;
        playerEXP = exp;
        playerMaxEXP = playerlevel[currentlevel];
        while (currentlevel < playerlevel.Count && exp >= playerlevel[currentlevel])
        {
            levelup();
        }
        UIController.Instance.UpdateEXPSlider();
    }

    public void levelup()
    {
        exp -= playerlevel[currentlevel];
        currentlevel++;
        playerEXP = exp;
        if (currentlevel < playerlevel.Count)
        {
            playerMaxEXP = playerlevel[currentlevel];
        }
        UIController.Instance.Leveluppanelopen();


    }

    public void ApplyUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Attack:
                skill1dmg += 0.5f;
                skill2dmg += 1f;
                skill1cd += 0.7f;
                skill2cd += 0.2f;
                Debug.Log("Attack boost!");
                break;

            case UpgradeType.Speed:
                movespeed += 0.5f;
                playerMaxHealth += 1f;
                Debug.Log("Speed up!");
                break;

            case UpgradeType.Heal:
                playerHealth = playerMaxHealth;
                if (playerHealth > playerMaxHealth) playerHealth = playerMaxHealth;
                UIController.Instance.UpdateHealthSlider();
                Debug.Log("Heal!");
                break;
        }
    }
}