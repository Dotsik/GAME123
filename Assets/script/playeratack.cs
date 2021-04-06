using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playeratack : MonoBehaviour
{
    public Camera cam;
    public int distanse;
    public Text damage_text;
    void Start()
    {
        
    }
       
    void Update()// атка по ЛКМ
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoAtack();
        }
    }

    private void DoAtack() // действие при атаке
    {   
        System.Random rand = new System.Random();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, distanse))
        {
            if(hit.collider.tag == "Enemy")
            {
				GetComponent<Animator>().SetTrigger("Attttttaack");
                damage_text.text = rand.Next(20,30).ToString();
                Enemy eHealth = hit.collider.GetComponent<Enemy>();
                eHealth.TakeDamage(Convert.ToInt32(damage_text.text));
                
            }
        }
    }
}
