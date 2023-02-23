using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferGameObject : MonoBehaviour
{
    public string destinationSceneName;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == destinationSceneName)
        {
            transform.parent = null;
            SceneManager.MoveGameObjectToScene(gameObject, scene);
        }
    }
}
