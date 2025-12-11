using UnityEngine;
using System.Collections.Generic;
public class Weapon : MonoBehaviour
{
    public int weaponlevel;
    public List<WeaponStats> stats;
    public Sprite weaponimage;

    public void Levelup()
    {
        if (weaponlevel < stats.Count - 1)
        {
            weaponlevel++;
        }
    } 
}

[System.Serializable]
public class WeaponStats
{
    public float cd;
    public float duration;
    public float damage;
    public float range;
    public float speed;
    public string descprition;
}
