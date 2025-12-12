using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] private int exptogive;
    void Start()
    {
        
    }

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
