using UnityEngine;

public class animation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Update()
    {
        Destroy(gameObject,animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
