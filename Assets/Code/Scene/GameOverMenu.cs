using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}
