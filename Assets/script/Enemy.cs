using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform player;
    public float move_speed;
    public float rotation_speed;
    public Transform enemy;
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
        
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
    }
}