using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public float Health;

    public float Armor;

    public void TakeDamage(float dmg)
    {
        Health -= dmg * Armor;
        Debug.Log(gameObject.name + " health = " + Health);
        if (Health <= 0)
        {
            //Explode
            Destroy(gameObject);
        }
    }
}
