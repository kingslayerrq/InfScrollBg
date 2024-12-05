using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IObserver
{
    public static ScoreManager Instance;
    public List<int> highScores = new List<int>();
    public int curScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void OnNotify()
    {
        throw new NotImplementedException();
    }
    private void OnEnable()
    {
        RunManager.Instance.AddObserver(this);
    }

    private void OnDisable()
    {
        RunManager.Instance.RemoveObserver(this);
    }

}
