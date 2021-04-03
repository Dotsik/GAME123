using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

public class enemyhealth : MonoBehaviour
{
    public GameObject Obj;
    private string trigger_Die_1 = "Die_1";
    private float health = 100;

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    public void TakeDamage(float dmg)
    {
        health -= dmg;
        

        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger(trigger_Die_1);
            StartCoroutine(KillOnAnimationEnd());
        }
    }
    void Update()
    {
       
    }

}
