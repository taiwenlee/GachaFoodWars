using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    protected Canvas pauseMenu;
    //public GameObject restartMenu;
    public static bool isPaused;
    public AudioSource pauseMenuSFX;
    public AudioMixer audioMixer;

    void Start()
    {

        pauseMenu = GetComponent<Canvas>();
        pauseMenu.enabled = false;
        //restartMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuSFX.Play();
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuSFX.Play();
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        pauseMenuSFX.Play();
        Time.timeScale = 1f;
        isPaused = false;
        Destroy(GameObject.FindGameObjectWithTag("Inventory"));
        SceneManager.LoadScene("Main Menu");
        GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>().enabled = false;
    }

    public void SetSFXVolume(float volume) {
        audioMixer.SetFloat("SFXVolume", volume);
   }

   public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
   }
   public void SetMasterVolume(float volume) {
        audioMixer.SetFloat("MasterVolume", volume);
   }

    public void QuitGame()
    {
        pauseMenuSFX.Play();
        Application.Quit();
    }
}
