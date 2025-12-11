using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    [SerializeField] private Rigidbody2D rb;
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

    [Header("Skills Stats")]
    public float skill1dmg = 2f;
    public float skill2dmg = 5f;
    public float skill1cd = 10f;
    public float skill2cd = 0f;

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

        // เช็ค null ก่อนเรียกใช้เพื่อความปลอดภัย
        if (UIController.Instance != null)
        {
            UIController.Instance.UpdateHealthSlider();
            UIController.Instance.UpdateEXPSlider();
        }
    }

    void Update()
    {
        // ... (Code การเดินเดิมของคุณ คงไว้เหมือนเดิม) ...
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        VelocityX *= friction;
        if (VelocityX > -0.05f && VelocityX < 0.05f) VelocityX = 0;
        if (VelocityX > -1f && VelocityX < 1f) VelocityX += InputX * 0.06f;

        VelocityY *= friction;
        if (VelocityY > -0.05f && VelocityY < 0.05f) VelocityY = 0;
        if (VelocityY > -1f && VelocityY < 1f) VelocityY += InputY * 0.06f;

        playerMoveDirection = new Vector3(VelocityX, VelocityY).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(VelocityX * movespeed, VelocityY * movespeed);
    }

    // ... (Functions TakeDamage, ApplySlow คงไว้เหมือนเดิม) ...
    public void ApplySlow(float duration)
    {
        movespeed = originalMoveSpeed * slowPercentage;
        slowDurationTimer = duration;
    }

    public void TakeDamage(float damage)
    {
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

        // เปิดหน้าต่าง UI Level Up
        UIController.Instance.Leveluppanelopen();

        // หมายเหตุ: ผมลบโค้ดส่วน activeweapon เดิมออกชั่วคราว 
        // เพราะเราจะใช้ปุ่มแบบ Fixed Card ตามรูปภาพของคุณแทน
    }

    // *** ฟังก์ชันใหม่สำหรับรับค่าจากปุ่ม Level Up ***
    public void ApplyUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Attack:
                skill1dmg += 2f;
                skill2dmg += 5f;
                Debug.Log("Attack boost!");
                break;

            case UpgradeType.Speed:
                movespeed += 1f;
                // update originalMoveSpeed ด้วยถ้ามีตัวแปรนี้
                Debug.Log("Speed up!");
                break;

            case UpgradeType.Heal:
                playerHealth += 20;
                if (playerHealth > playerMaxHealth) playerHealth = playerMaxHealth;
                UIController.Instance.UpdateHealthSlider();
                Debug.Log("Heal!");
                break;
        }
    }
}