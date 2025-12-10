using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [SerializeField] private float HealAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Heal");
            Controller.Instance.TakeDamage(-1f*HealAmount);
            Destroy(gameObject);
        }
    }
}
