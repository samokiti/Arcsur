using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public GameObject Projectile;
    public int summontime = 200;

    void FixedUpdate()
    {
        
        summontime --;
        if (summontime <0)
        {
            summontime = 180;
            
            Instantiate(Projectile, transform.position, transform.rotation);
        }

    }



}
