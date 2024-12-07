using UnityEngine;
using UnityEngine.Events;

public class Player : Subject, IObserver
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _curHealth;
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
        if (_curHealth == 0)
        {
            // TODO: End Run
            RunManager.Instance.EndRun();
        }
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
