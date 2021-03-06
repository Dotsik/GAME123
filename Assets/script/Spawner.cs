using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject Cube;
    public float timer;
    public Transform player;
    public float move_speed;
    public float rotation_speed;
    public Transform enemy;
    public Vector3 vector3;
    
   
    IEnumerator SpawnCD()
    {
        //var cube = Instantiate<GameObject>(Cube);

        vector3 = new Vector3(10, 0, 10);
        yield return new WaitForSeconds(timer);
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
        Instantiate(Cube, player.position+vector3, Quaternion.identity);
        Repeat();
    }

    void Start()
    {
        StartCoroutine(SpawnCD());
    }

    void Repeat()
    {
        StartCoroutine(SpawnCD());
    }
    
    void Update()
    {
        
    }

}
