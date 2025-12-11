using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public GameObject Projectile;
    private int summontime = 150;

    void FixedUpdate()
    {
        
        summontime --;
        if (summontime <0)
        {
            summontime = 150;
            
            Instantiate(Projectile, transform.position, transform.rotation);
        }

    }



}
