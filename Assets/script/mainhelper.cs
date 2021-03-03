using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainhelper : MonoBehaviour
{
    public Button Play;
    public Button Option;
    public Button ExitGame;


    void Start()
    {
        Play.onClick.AddListener(ToGame);
        Option.onClick.AddListener(Open_option);
        ExitGame.onClick.AddListener(Exit);
    }

    void ToGame()
    {
        SceneManager.LoadScene("main");
    }

    void Open_option()
    {
        SceneManager.LoadScene("Option");
    }

    void Exit()
    {
        Application.Quit();
    }

}
