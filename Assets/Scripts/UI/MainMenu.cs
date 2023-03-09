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

    private string _nextScene;

    public void PlayGame(){
        MenuSFX.Play();
        if (!pprog.TrainingComplete)
        {
            _nextScene = trainingScene;
            pprog.TrainingComplete = true;
        }
        else
        {
            _nextScene = restScene;
        }

        SceneManager.LoadScene(_nextScene);
        Time.timeScale = 1f;
        //GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>().enabled = true;
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
