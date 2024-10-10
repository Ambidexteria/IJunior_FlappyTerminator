using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageProgressBar _stageProgressBar;
    [SerializeField] private Bird _bird;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnCooldown = 2f;
    [SerializeField] private TextMeshProUGUI _endStageMessage;
    [SerializeField] private string _gameOverText = "Game Over";
    [SerializeField] private string _winText = "You win!";

    private WaitForSeconds _waitStartSpawnDelay;
    private WaitForSeconds _waitSpawnCooldown;

    private void OnEnable()
    {
        _bird.Dead += ShowGameOverMessage;
        _bird.Win += ShowWinMessage;
    }

    private void OnDisable()
    {
        _bird.Dead -= ShowGameOverMessage;
        _bird.Win -= ShowWinMessage;
    }

    private void Awake()
    {
        if (_enemySpawner == null)
            throw new NullReferenceException();

        if (_stageProgressBar == null)
            throw new NullReferenceException();

        if (_bird == null)
            throw new NullReferenceException();

        if (_endStageMessage == null)
            throw new NullReferenceException();

        if (_spawnDelay <= 0)
            throw new ArgumentOutOfRangeException();

        if (_spawnCooldown <= 0)
            throw new ArgumentOutOfRangeException();

        _waitStartSpawnDelay = new WaitForSeconds(_spawnDelay);
        _waitSpawnCooldown = new WaitForSeconds(_spawnCooldown);
    }

    private void Start()
    {
        StartCoroutine(LaunchSpawnCoroutine());
    }

    private void Update()
    {
        _stageProgressBar.UpdateProgress(_bird.transform.position);
    }

    private IEnumerator LaunchSpawnCoroutine()
    {
        yield return _waitStartSpawnDelay;

        while (enabled)
        {
            _enemySpawner.Spawn();
            yield return _waitSpawnCooldown;
        }
    }

    private void ShowGameOverMessage()
    {
        Time.timeScale = 0f;
        _endStageMessage.text = _gameOverText;
        _endStageMessage.gameObject.SetActive(true);
    }

    private void ShowWinMessage()
    {
        Time.timeScale = 0f;
        _endStageMessage.text = _winText;
        _endStageMessage.gameObject.SetActive(true);
    }
}
