using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageProgressBar _stageProgressBar;
    [SerializeField] private Bird _bird;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnCooldown = 2f;
    [SerializeField] private TextMeshProUGUI _endStageMessage;
    [SerializeField] private ButtonClickHandler _playButton;
    [SerializeField] private ButtonClickHandler _restartButton;
    [SerializeField] private string _gameOverText = "Game Over";
    [SerializeField] private string _winText = "You win!";

    private WaitForSeconds _waitStartSpawnDelay;
    private WaitForSeconds _waitSpawnCooldown;
    private Scene _activeScene;

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

        if(_playButton == null)
            throw new NullReferenceException();

        if(_restartButton == null)
            throw new NullReferenceException();

        if (_spawnDelay <= 0)
            throw new ArgumentOutOfRangeException();

        if (_spawnCooldown <= 0)
            throw new ArgumentOutOfRangeException();

        Pause();

        _activeScene = SceneManager.GetActiveScene();
        _waitStartSpawnDelay = new WaitForSeconds(_spawnDelay);
        _waitSpawnCooldown = new WaitForSeconds(_spawnCooldown);
    }

    private void OnEnable()
    {
        _bird.Dead += ShowGameOverMessage;
        _bird.Dead += Pause;
        _bird.Winning += ShowWinMessage;
        _bird.Winning += Pause;

        _playButton.Clicked += Play;
        _restartButton.Clicked += Restart;
    }

    private void OnDisable()
    {
        _bird.Dead -= ShowGameOverMessage;
        _bird.Dead -= Pause;
        _bird.Winning -= ShowWinMessage;
        _bird.Winning -= Pause;

        _playButton.Clicked -= Play;
        _restartButton.Clicked -= Restart;
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
        _endStageMessage.text = _gameOverText;
        _endStageMessage.gameObject.SetActive(true);

        Pause();
        _restartButton.gameObject.SetActive(true);
    }

    private void ShowWinMessage()
    {
        _endStageMessage.text = _winText;
        _endStageMessage.gameObject.SetActive(true);

        Pause();
        _restartButton.gameObject.SetActive(true);
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void Play()
    {
        Time.timeScale = 1;
        _playButton.gameObject.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene(_activeScene.buildIndex);
    }
}
