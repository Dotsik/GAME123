using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratack : MonoBehaviour
{
    public Camera cam;
    public int distanse;
    void Start()
    {
        
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

        if(Physics.Raycast(ray,out hit, distanse))
        {
            if(hit.collider.tag == "Enemy")
            {
                Enemy eHealth = hit.collider.GetComponent<Enemy>();
                eHealth.TakeDamage(25);
            }
        }
    }
}
