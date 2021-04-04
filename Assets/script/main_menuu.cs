using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_menuu : MonoBehaviour
{
    public Button start;
    public Button Option;
    public Button Continue;
    public Button Exit;
    public Button Back;

    public void starting(){
        SceneManager.LoadScene("main");
    }

    public void contining(){

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
}
