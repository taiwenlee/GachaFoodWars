using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Rest");
        Time.timeScale = 1f;
        GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>().enabled = true;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
