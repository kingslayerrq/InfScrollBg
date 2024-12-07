using System;
using UnityEngine;

public class BackgroundManager: MonoBehaviour, IObserver
{
    public static BackgroundManager Instance;
    public float scrollSpeed;
    [Tooltip("X position which triggers the background to snap back to its starting position for seamless bg transition")]
    public float resetPosition;
    [Tooltip("X position where the background scroll starts")]
    public float startPosition;
    public bool isScrolling = false;
    public GameObject background;

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
        RunManager.Instance.AddObserver(Instance);
    }

    void Update()
    {
        if (isScrolling)
        {
            background.transform.Translate( scrollSpeed * Time.deltaTime * Vector3.left);
            if (background.transform.position.x < resetPosition)
            {
                background.transform.position = new Vector3(startPosition, background.transform.position.y);
            }
            //Debug.Log(background.transform.position.x);
        }
    }

    

    private void OnDisable()
    {
        RunManager.Instance.RemoveObserver(Instance);
    }

    public void OnNotify(string sceneName)
    {
        switch (sceneName)
        {
            case "MenuScene":
                break;
            case "GameScene":
                Debug.Log("BG notified in GameScene");
                isScrolling = true;
                break;
            case "EndScene":
                Debug.Log("BG notified in EndScene");
                isScrolling = false;
                break;
            default:
                break;
        }
    }
}
