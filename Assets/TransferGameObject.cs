using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferGameObject : MonoBehaviour
{
    public string[] destinationSceneNames;

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
        foreach (var destinationSceneName in destinationSceneNames)
        {
            if (scene.name == destinationSceneName)
            {
                transform.parent = null;
                SceneManager.MoveGameObjectToScene(gameObject, scene);
            }
        }
    }
}