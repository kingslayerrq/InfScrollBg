using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Player : Subject, IObserver
{
    [SerializeField] private int _maxHealth = 2;
    [SerializeField] private int _curHealth;
    [SerializeField] private float recoverHealthWaitTime;
    private Animator _playerAnimator;
    public string animBoolName;
    public bool isRunning = false;
    public UnityEvent onPlayerDeath;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        RunManager.Instance.AddObserver(this);
    }

    public void OnNotify(string sceneName)
    {
        switch (sceneName)
        {
            case "MenuScene":
                break;
            case "GameScene":
                Debug.Log("Player notified in GameScene");
                ResetPlayer();
                _playerAnimator.SetBool(animBoolName, isRunning);
                break;
            case "EndScene":
                Debug.Log("Player notified in EndScene");
                isRunning = false;
                _playerAnimator.SetBool(animBoolName, isRunning);
                break;
            default:
                break;
        }
    }

    public void TakeDmg(int amount)
    {
        Debug.Log("player hit");
        _curHealth -= amount;
        // Start Coroutine
        StartCoroutine(RecoverHealth(recoverHealthWaitTime));
        if (_curHealth == 0)
        {
            // TODO: End Run
            RunManager.Instance.EndRun();
        }
        // 
    }

    private IEnumerator RecoverHealth(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // Recover
        Recover(1);
    }

    private void Recover(int amount)
    {
        _curHealth += amount;
        _curHealth = Mathf.Min(_maxHealth, _curHealth);             // Clamp below max health
    }
    
    private void ResetPlayer()
    {
        _curHealth = _maxHealth;
        isRunning = true;
    }
    
    private void OnDestroy()
    {
        RunManager.Instance.RemoveObserver(this);
    }
}
