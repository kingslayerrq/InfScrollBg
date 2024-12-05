using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Subject
{
    public static GameManager Instance;
    public string menuScene;
    public string gameScene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    // Loads a scene 
    public void LoadScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
    
}
