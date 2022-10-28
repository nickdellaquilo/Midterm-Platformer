using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update() 
    {
        if (Input.GetButtonDown("START")) { Play(); }
        if (Input.GetButtonDown("BACK"))  { Quit(); }
    }


    public void Play(){
        SceneManager.LoadScene("Level1");
    }

    public void Quit(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}
