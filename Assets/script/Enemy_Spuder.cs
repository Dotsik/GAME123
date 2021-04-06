using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spuder : MonoBehaviour
{
    public Transform player; //Transform игрока
    public float move_speed; //МС врага
    public float rotation_speed; //РС врага
    public Transform enemy; //Transform врага
    public GameObject GOEnemy; // GameObject врага;
    public float timer; //Таймер
    private int health = 1000; //ХП врага
    private Vector3 vector3, vector2; //Вектора хз как работает
    private static int temp= 1;
    private static int points = 0;//Очки
    private IEnumerator Attackdelay() //Функция задержки удара
    {
        yield return new WaitForSeconds(0.9f);
        this.move_speed = 7;
        this.rotation_speed = 7;
    }

    private IEnumerator KillOnAnimationEnd() //Функция уничтожения
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
        points += 1000;
    }

    public void TakeDamage(int dmg) //Анимация смерти 
    {
        health -= dmg;

        if (health <= 0)
        {
            //GetComponent<Animator>().SetTrigger(trigger_Die_1);
            this.move_speed = 0;
            this.rotation_speed = 0;
            StartCoroutine(KillOnAnimationEnd());
        }
    }

    void Update() //Функция передвижения врага
    {
        var look_dir = player.position - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir),
            rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;
        
    }

   Vector3 RandomBetweenRadius2D(float minRad, float y, float maxRad) //хз как работает
    {
        float radius = UnityEngine.Random.Range(minRad, maxRad);
        float angle = UnityEngine.Random.Range(0, 360);

        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

        return new Vector3(x, y, z);
    }

    IEnumerator SpawnCD() //Спавнер
    {
        if(temp==1)
        {
        vector3 = RandomBetweenRadius2D(80, player.position.y, 100);
            yield return new WaitForSeconds(timer);
            var look_dir = player.position - enemy.position;
            look_dir.y = 0;
            enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir),
                rotation_speed * Time.deltaTime);
            enemy.position += enemy.forward * move_speed * Time.deltaTime;
            var children = Instantiate(GOEnemy, vector3, Quaternion.identity) as GameObject;
            children.GetComponent<Enemy>().enabled = true;
        }
        else
            Debug.Log("lox");



}

    void Start()
    {
        StartCoroutine(SpawnCD());
    }
}
