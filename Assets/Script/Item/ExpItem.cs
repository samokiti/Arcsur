using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] private int exptogive;
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
            Controller.Instance.getexp(exptogive);
            Destroy(gameObject);
        }
    }
}
