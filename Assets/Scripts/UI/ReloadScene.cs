using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GetActiveSceneExample : MonoBehaviour
{
    public Scene currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void Click()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name, LoadSceneMode.Single);
    }
}