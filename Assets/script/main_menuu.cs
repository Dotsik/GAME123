using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class main_menuu : MonoBehaviour
{
    public void load_new_game()
    {
        SceneManager.LoadScene("main");
    }
    public void load_game()
    {

    }
    public void settings()
    {
        SceneManager.LoadScene("parametrs");
    }
    public void exit()
    {

    }
}
