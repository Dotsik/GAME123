using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{

    public Transform player;//Transform игрока
    public float move_speed;//МС врага
    public float rotation_speed;//РС врага
    public Transform enemy;//Transform врага
    public GameObject GOEnemy;// GameObject врага;
    public float timer;//Таймер
    private float timer_to_new_Vawe = 10f;//Таймер к новой волне
    private string trigger_Die_1 = "Die_1";//Триггер смерти врага
    private string trigger_Attack_zone = "Attack_zone";//Триггер атаки врага
    private int health = 100; //ХП врага
    private Vector3 vector3, vector2;//Вектора хз как работает
    private static int[] Vawes = new int[] { 10, 10, 10, 10};//Волны
    private int counter, cer = 0;
    
    public Text i;
    
    private IEnumerator Attackdelay()//Функция задержки удара
    {
        yield return new WaitForSeconds(0.9f);
        this.move_speed = 7;
        this.rotation_speed = 7; 
    }
    
    private IEnumerator KillOnAnimationEnd()//Функция уничтожения
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
    
    public void TakeDamage(int dmg)//Анимация смерти 
    {
        health -= dmg;
        
        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger(trigger_Die_1);
            this.move_speed = 0;
            this.rotation_speed = 0;
            StartCoroutine(KillOnAnimationEnd());
        }
    }
    
    void Update()//Функция передвижения врага
    {
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
    }
    
    void OnTriggerStay(Collider col)//Анимация удара 
    {
        if (col.tag == "Trigger_Alien_Attack_Hit")
        {
            GetComponent<Animator>().SetTrigger(trigger_Attack_zone);
            this.move_speed = 2;
            this.rotation_speed = 3;
        }
    }
   
    void OnTriggerExit(Collider col)//Анимация задержки удара
    {
        if (col.tag == "Trigger_Alien_Attack_Hit")
        {
            StartCoroutine(Attackdelay());
        }
    }
    
   Vector3 RandomBetweenRadius2D(float minRad,float y, float maxRad)//хз как работает
    {
        float radius = UnityEngine.Random.Range(minRad, maxRad);
        float angle = UnityEngine.Random.Range(0, 360);

        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

        return new Vector3(x, y, z);
    }

   IEnumerator SpawnCD()//Спавнер
   {
       
       
       if (Vawes[cer] < 1)
       {
          ++cer;
          Start();
          yield return new WaitForSeconds(timer_to_new_Vawe);
          Debug.Log(cer);
       }
       else
       {
          
           Vawes[cer]--;
            Debug.Log(Vawes[cer]);
           vector3 = RandomBetweenRadius2D(60, player.position.y, 100);
           yield return new WaitForSeconds(timer);
           var look_dir = player.position - enemy.position;
           look_dir.y = 0;
           enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir),
               rotation_speed * Time.deltaTime);
           enemy.position += enemy.forward * move_speed * Time.deltaTime;
           var children = Instantiate(GOEnemy, vector3, Quaternion.identity) as GameObject;
           children.GetComponent<Enemy>().enabled = true;
           
       }

   }
   
   
   
    void Start()
    {
           StartCoroutine(SpawnCD()); 
    }
    
    
}