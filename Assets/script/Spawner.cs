using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{

    public GameObject Cube;
    public float timer;
    public Transform player;
    public float move_speed;
    public float rotation_speed;
    public Transform enemy;
    public Vector3 vector3, vector2;
  


    Vector3 RandomBetweenRadius2D(float minRad,float y, float maxRad)
    {
        float radius = UnityEngine.Random.Range(minRad, maxRad);
        float angle = UnityEngine.Random.Range(0, 360);

        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

        return new Vector3(x, y, z);
    }

    IEnumerator SpawnCD()
    {

        vector3 = RandomBetweenRadius2D(60, player.position.y, 100);

        yield return new WaitForSeconds(timer);
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
        var children = Instantiate(Cube, vector3, Quaternion.identity) as GameObject;
        children.GetComponent<Enemy>().enabled=true;
        //Repeat();
        
    }

    void Start()
    {

        StartCoroutine(SpawnCD());
    }

    //void Repeat()
   // {
   //     StartCoroutine(SpawnCD());
   // }

    void Update()
    {
        
    }

}
