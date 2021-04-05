using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, distanse))
        {
            if(hit.collider.tag == "Enemy")
            {
                damage_text.text = 25.ToString();
                Enemy eHealth = hit.collider.GetComponent<Enemy>();
                eHealth.TakeDamage(25);
            }
        }
    }
}
