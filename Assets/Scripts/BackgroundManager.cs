using System;
using UnityEngine;

public class BackgroundManager: MonoBehaviour, IObserver
{
    public float scrollSpeed;
    [Tooltip("X position which triggers the background to snap back to its starting position for seamless bg transition")]
    public float resetPosition;
    [Tooltip("X position where the background scroll starts")]
    public float startPosition;
    public bool isScrolling = false;
    
    void Update()
    {
        if (isScrolling)
        {
            transform.Translate( scrollSpeed * Time.deltaTime * Vector3.left);
            if (transform.position.x < resetPosition)
            {
                transform.position = new Vector3(startPosition, transform.position.y);
            }
            Debug.Log(transform.position.x);
        }
    }

    private void OnEnable()
    {
        RunManager.Instance.AddObserver(this);
    }

    private void OnDisable()
    {
        RunManager.Instance.RemoveObserver(this);
    }

    public void OnNotify()
    {
        // Start scrolling when the run starts
        isScrolling = true;
    }
}
