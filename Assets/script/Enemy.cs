using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform player;
    public float move_speed;
    public float rotation_speed;
    public float healht;
    public Transform enemy;
    public Enemy(float a)
    {
        healht = a;
    }
    void Update()
    {
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
    }

    public void OnCollisionEnter()
    {
        healht = this.healht - 5;
        if (healht <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"), .5f);
        }
    }
}