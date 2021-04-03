using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public GameObject Obj;
    public KeyCode playAnimKey = KeyCode.Mouse0;
    public string triggerName;

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
        if (col.tag == "Trigger_Alien_Attack_Hit")
        {
            Obj.GetComponent<Animator>().Play("Armature|Attack_Hit");
            
        }
    
    }
    //yield return new WaitForSeconds(1f);
    //return;
}
