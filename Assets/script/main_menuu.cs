using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_menuu : MonoBehaviour
{
    public Button start;
    public Button Option;
    public Button Exit;
    public Button Back;
    public Button GG;

    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("main_menu");
        }
    }

    public void starting(){
        SceneManager.LoadScene("main");
    }

    public void setting(){
        SceneManager.LoadScene("parametrs");
    }

    public void exit(){
        Application.Quit();
    }

    public void Backk(){
        SceneManager.LoadScene("main_menu");
    }

    public void GG_WP(){
        SceneManager.LoadScene("main_menu");
    }
}
