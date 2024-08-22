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

    private WaitForSeconds _wait;

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

        if (_spawnDelay <= 0 || _spawnCooldown <= 0)
            throw new ArgumentOutOfRangeException();
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
        yield return new WaitForSeconds(_spawnDelay);

        _wait = new WaitForSeconds(_spawnCooldown);

        while (enabled)
        {
            _enemySpawner.Spawn();
            yield return _wait;
        }
    }

    private void ShowGameOverMessage()
    {
        Time.timeScale = 0f;
        _endStageMessage.text = "Game Over";
        _endStageMessage.gameObject.SetActive(true);
    }

    private void ShowWinMessage()
    {
        Time.timeScale = 0f;
        _endStageMessage.text = "You win!";
        _endStageMessage.gameObject.SetActive(true);
    }
}
