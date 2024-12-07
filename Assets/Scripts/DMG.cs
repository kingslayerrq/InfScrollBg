using System;
using UnityEngine;

public class DMG : Hostile
{
    [SerializeField] private int _dmg = 1;
    [SerializeField] private float _difficultySpeedMultiplier = 0.1f;
    [SerializeField] private float _currentSpeed ;
    [Tooltip("Distance pass player that triggers self clean up")]
    [SerializeField] private float _cleanUpDistance;
    

    [SerializeField] private GameObject _player;
    private Transform _playerTransform;

    private void OnEnable()
    {
        _currentSpeed = baseSpeed;
        if (!_player)
        {
            _player = GameObject.FindWithTag("Player");
        }
        _playerTransform = _player.GetComponent<Transform>();
    }

    private void Update()
    {
        if (shouldStop) return;
        
        _currentSpeed = baseSpeed * ObstacleSpawner.Instance.currentDifficulty * _difficultySpeedMultiplier;
        transform.Translate(Vector3.left * _currentSpeed);
        if (_playerTransform && transform.position.x < _playerTransform.position.x - _cleanUpDistance)
        {
            CleanUp();
        }
    }

    // Remove reference from list then destroy
    private void CleanUp()
    {
        ObstacleSpawner.Instance.spawnedObstacles.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colliding " + other.gameObject.name);
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDmg(_dmg);
            CleanUp();
        }
    }

}
