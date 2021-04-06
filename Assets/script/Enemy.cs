using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public Transform player;//Transform игрока
    public float move_speed;//МС врага
    public float rotation_speed;//РС врага
    public Transform enemy;//Transform врага
    public GameObject GOEnemy;// GameObject врага;
    public float timer;//Таймер
    private string trigger_Die_1 = "Die_1";//Триггер смерти врага
    private string trigger_Attack_zone = "Attack_zone";//Триггер атаки врага
    private int health = 200; //ХП врага
    private static int points = 0;//Очки
    private Vector3 vector3, vector2;//Вектора хз как работает
    public static int Vawe = 25;//Волны
    private int counter;
    public static int temp = 0,temp1=1,temp2=0;
   public Text Vawe_info,Points_info;
   public bool winn = false;
   public Text volna;
   public Text volna2;
   public float dd = 10;
   public Slider healthSlider;
   public float currentHealth = 100;
    
    
    private IEnumerator Attackdelay()//Функция задержки удара
    {
        yield return new WaitForSeconds(0.9f);
        this.move_speed = 10;
        this.rotation_speed = 10; 
    }
    private IEnumerator Delay()//Функция задержки удара
    {
        yield return new WaitForSeconds(0.9f);
        
    }
    
    private IEnumerator KillOnAnimationEnd()//Функция уничтожения
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
        points += 25;
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
        Points_info.text = "Points - " + points.ToString();
        Vawe_info.text="To next - " + (GameObject.FindGameObjectsWithTag("Enemy").Length-1).ToString();
        if ((GameObject.FindGameObjectsWithTag("Enemy").Length-1)==0)
        {
            if(winn == true){
                    SceneManager.LoadScene("win");
                }
            if (temp == 1)
            {
                volna.enabled = false;
                volna2.enabled = true;
                Vawe = 50;
                Start();
                temp = 0;
                temp2 = 1;
                
            }
            else if (temp1 == 0 && temp2==1 )
            {
                winn = true;
            }
        }
        
    }
    public void TakeDamage(float amount)
    {
        Delay();
        currentHealth-=amount;	
        Debug.Log(healthSlider.value);
        healthSlider.value -= amount;
        
				
        //if (healthSlider.value <= 0 && !isDead) //если умер
       // {
       //     Death();
       // }
       // return healthSlider.value;
    }
    void OnTriggerEnter(Collider col)//Анимация удара 
    {
        if (col.tag == "Trigger_Alien_Attack_Hit")
        {
            TakeDamage(dd);
        }
    }
    void OnTriggerStay(Collider col)//Анимация удара 
    {
        if (col.tag == "Trigger_Alien_Attack_Hit")
        {
            GetComponent<Animator>().SetTrigger(trigger_Attack_zone);
            this.move_speed = 2;
            this.rotation_speed = 3;
            //new SC_FPSController().TakeDamage(dd);
            
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

       if (Vawe > 0)
       {
           vector3 = RandomBetweenRadius2D(100, player.position.y, 120);
           yield return new WaitForSeconds(timer);
           var look_dir = player.position - enemy.position;
           look_dir.y = 0;
           enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir),
               rotation_speed * Time.deltaTime);
           enemy.position += enemy.forward * move_speed * Time.deltaTime;
           var children = Instantiate(GOEnemy, vector3, Quaternion.identity) as GameObject;
           children.GetComponent<Enemy>().enabled = true;
           Vawe--;
       }
       else if (temp1 == 1)
       {
           temp = 1;
           temp1 = 0;
       }


   }
   
    void Start()
    {
           StartCoroutine(SpawnCD()); 
    }
    
    
}