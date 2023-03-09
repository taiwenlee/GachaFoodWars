using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public GameObject restartMenu;
   


    private void Start()
    {
        
        restartMenu.SetActive(false);
    }

    public void Restart()
    {
        
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
        SceneManager.LoadScene("Main Menu");
        restartMenu.SetActive(false);
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }
}
