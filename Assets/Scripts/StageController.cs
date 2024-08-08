using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageController : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnCooldown = 2f;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    private WaitForSeconds _wait;

    private void OnEnable()
    {
        _bird.Dead += GameOver;
    }

    private void OnDisable()
    {
        _bird.Dead -= GameOver;
    }

    private void Awake()
    {
        if (_enemySpawner == null)
            throw new NullReferenceException();

        if (_spawnDelay <= 0 || _spawnCooldown <= 0)
            throw new ArgumentOutOfRangeException();
    }

    private void Start()
    {
        StartCoroutine(LaunchSpawnCoroutine());
    }

    private IEnumerator LaunchSpawnCoroutine()
    {
        yield return new WaitForSeconds(_spawnDelay);

        _wait = new WaitForSeconds(_spawnCooldown);

        while (enabled)
        {
            _enemySpawner.SpawnAtRandomPosition();
            yield return _wait;
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        _gameOverText.gameObject.SetActive(true);
    }
}
