using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhealth : MonoBehaviour
{
    public float health = 25;
    public void TakeDamage(float amnt)
    {
        health -= amnt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
