using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratack : MonoBehaviour
{
    public Camera cam;
    public GameObject Hand;
    public weapon myweapon;
    void Start()
    {
        myweapon = Hand.GetComponentInChildren<weapon>();
    }

    
    void Update()// атка по ПКМ
    {
        if (Input.GetMouseButtonDown(1))
        {
            DoAtack();
        }
    }
    private void DoAtack() // действие при атаке
    {   
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, myweapon.atackrange))
        {
            if(hit.collider.tag == "Enemy")
            {
                enemyhealth eHealth = hit.collider.GetComponent<enemyhealth>();
                eHealth.TakeDamage(myweapon.atackdamage);
            }
        }
    }
}
