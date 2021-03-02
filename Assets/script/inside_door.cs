using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inside_door : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("inside");
    }
}