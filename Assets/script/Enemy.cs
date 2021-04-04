using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform player;
    public float move_speed;
    public float rotation_speed;
    public Transform enemy;
    //public Animation anim;

    void Update()
    {
        
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
      // if (!(enemy.position.x > player.position.x + 2 && enemy.position.z > player.position.z + 2) || !(enemy.position.y < player.position.y + 2 && enemy.position.y < player.position.y + 2))
      // {
      //     anim = GetComponent<Motion>();
      //     anim.Play("Armature|Attak_Hit");
      // }
    }
}