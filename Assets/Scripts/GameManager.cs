using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Subject
{
    public static GameManager Instance;
    public string menuScene = "MenuScene";
    public string gameScene = "GameScene";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForGameInit());
    }

    private IEnumerator WaitForGameInit()
    {
        yield return new WaitForSeconds(0f);
        NotifyObserver("MenuScene");
    }
    
    // Loads a scene 
    public void LoadScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        if (newScene.name == gameScene)
        {
            Debug.Log("Game Scene loaded");
            NotifyObserver(newScene.name);
        }
    }
}
