using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private Animator animator;
    [SerializeField] private float movespeed;
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

    public Weapon activeweapon;

    public float skill1dmg = 2f;
    public float skill2dmg = 5f;
    public float skill1cd = 10f;
    public float skill2cd = 1f;

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
        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.UpdateEXPSlider();

    }
    void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        VelocityX *= friction;
        if (VelocityX > -0.05f && VelocityX < 0.05f)
        {
            VelocityX = 0;
        }
        if (VelocityX > -1f && VelocityX < 1f)
            VelocityX += InputX * 0.06f;

        VelocityY *= friction;
        if (VelocityY > -0.05f && VelocityY < 0.05f)
        {
            VelocityY = 0;
        }

        if (VelocityY > -1f && VelocityY < 1f)
            VelocityY += InputY * 0.06f;


        playerMoveDirection = new Vector3(VelocityX, VelocityY).normalized;

        //animator.SetFloat("moveX", VelocityX);
        //animator.SetFloat("moveY", VelocityY);

        if (playerMoveDirection == Vector3.zero)
        {
            //animator.SetBool("moving", false);
        }
        else
        {
            //animator.SetBool("moving", true);
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(VelocityX * movespeed, VelocityY * movespeed);
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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TriggerGameOver();
            }
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
        if (activeweapon != null && UIController.Instance.levelupbuttons.Count > 0)
        {
            UIController.Instance.levelupbuttons[0].Activatbutton(activeweapon);
        }
        UIController.Instance.Leveluppanelopen();
    }
}
