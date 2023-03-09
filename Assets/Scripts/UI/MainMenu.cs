using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string trainingScene; // instruction room string name
    [SerializeField] string restScene; // rest string name 
    [SerializeField] PlayerProgression pprog;
    public AudioSource MenuSFX;

    public void PlayGame(){
        MenuSFX.Play();

        SceneManager.LoadScene("Rest");
        Time.timeScale = 1f;
        GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>().enabled = true;
    }

    public void OptionsButton(){
        MenuSFX.Play();
        SceneManager.LoadScene("OptionsScene");
    }

    public void TutorialRoom(){
        MenuSFX.Play();
        SceneManager.LoadScene("Instruction Room");
    }

    public void QuitGame(){
        MenuSFX.Play();
        Application.Quit();
    }
}
