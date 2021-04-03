using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform player;//Transform игрока
    public float move_speed;//МС врага
    public float rotation_speed;//РС врага
    public Transform enemy;//Transform врага
    private string trigger_Die_1 = "Die_1";//Триггер смерти врага
    private string trigger_Attack_zone = "Attack_zone";//Триггер атаки врага
    private float health = 100; //ХП врага
    
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
    public void TakeDamage(float dmg)//Анимация смерти 
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
}