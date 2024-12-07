using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour, IObserver
{
    public static ScoreBoardManager Instance;
    public List<int> highScores = new List<int>();
    public int highestScore;
    public TextMeshProUGUI highScoreUI;

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
        GameManager.Instance.AddObserver(Instance);
    }

    public void OnNotify(string sceneName)
    {
        switch (sceneName)
        {
            case "MenuScene":
                HandleMenuSceneScoreBoardManagerAction();
                break;
            case "GameScene":
                HandleGameSceneScoreBoardManagerAction();
                break;
            case "EndScene":
                HandleEndSceneScoreBoardManagerAction();
                break;
            default:
                break;
        }
    }

    // Update and Show the highest score in menu
    private void HandleMenuSceneScoreBoardManagerAction()
    {
        highScoreUI = GameObject.Find("HighScoreUI").GetComponent<TextMeshProUGUI>();
        if (highScores.Count == 0)
        {
            highScoreUI.enabled = false;
            return;
        }
        Debug.Log("Update&&Showing HighestScore");
        UpdateHighestScore();
        highScoreUI.text = highestScore.ToString();
    }
    private void HandleGameSceneScoreBoardManagerAction()
    {
        
    }
    
    private void HandleEndSceneScoreBoardManagerAction()
    {
        
    }

    
    private void UpdateHighestScore()
    {
        highestScore = highScores.Max();
    }

    private void OnDisable()
    {
        GameManager.Instance.RemoveObserver(Instance);
    }
}
