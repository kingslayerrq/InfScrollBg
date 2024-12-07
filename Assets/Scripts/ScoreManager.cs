using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IObserver
{
    public static ScoreManager Instance;
    public bool isScoring = false;
    public int curScore = 0;
    public Player player;
    public TextMeshProUGUI curScoreUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);                                    // Maybe we dont need this
        }
    }

    private void Start()
    {
        RunManager.Instance.AddObserver(Instance);
    }

    private void Update()
    {
        if (isScoring)
        {
            // TODO: Your scoring mechanism here
            // On Score change call the UpdateScoreUI
            // Example
            curScore++;
            UpdateScoreUI(curScore);
        }
    }

    private void UpdateScoreUI(int score)
    {
        if (!curScoreUI) curScoreUI = GameObject.Find("CurScoreUI").GetComponent<TextMeshProUGUI>();
        curScoreUI.text = score.ToString();
    }

    public void OnNotify(string sceneName)
    {
        switch (sceneName)
        {
            case "MenuScene":
                HandleMenuSceneScoreManagerAction();
                break;
            case "GameScene":
                HandleGameSceneScoreManagerAction();
                break;
            case "EndScene":
                HandleEndSceneScoreManagerAction();
                break;
            default:
                break;
        }
    }

    private void HandleMenuSceneScoreManagerAction()
    {
        
    }

    // Start scoring
    private void HandleGameSceneScoreManagerAction()
    {
        curScore = 0;
        // Get a reference on scene loads
        if(!curScoreUI) curScoreUI = GameObject.Find("CurScoreUI").GetComponent<TextMeshProUGUI>();
        isScoring = true;
    }
    
    // Stops recording of score and add to highscore list
    private void HandleEndSceneScoreManagerAction()
    {
        isScoring = false;
        ScoreBoardManager.Instance.highScores.Add(curScore);
        // TODO: Maybe enlarge this run's score
        
    }

    

   

    private void OnDisable()
    {
        RunManager.Instance.RemoveObserver(Instance);
    }

}
