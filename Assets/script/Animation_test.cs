using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_test : MonoBehaviour
{
    public GameObject Obj;
    public Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Obj.GetComponent<Animation>().Play("Armature|Die_1");
            Destroy(enemy);
        }
    
    }
}
