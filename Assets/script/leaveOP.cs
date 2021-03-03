using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class leaveOP : MonoBehaviour
{
    public Button Back;

    void Start()
    {
        Back.onClick.AddListener(Backk);
    }

    void Backk()
    {
        SceneManager.LoadScene("main_menu");
    }
    
}
