using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Rest");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
