using System;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private StageFinish _stageFinish;
    [SerializeField] private StageStart _stageStart;

    private float _stageDistance;

    private void Awake()
    {
        if (_slider == null)
            throw new NullReferenceException();

        if (_stageFinish == null)
            throw new NullReferenceException();

        if (_stageFinish == null)
            throw new NullReferenceException();

        CalculateStageDistance();
    }

    public void UpdateProgress(Vector2 playerPosition)
    {
        if (playerPosition.x < _stageStart.transform.position.x)
        {
            _slider.value = 0;
        }
        else
        {
            float distanceFromStart = playerPosition.x - _stageStart.transform.position.x;
            float value = Mathf.Clamp01(distanceFromStart / _stageDistance);
            _slider.value = value;
        }
    }

    private void CalculateStageDistance()
    {
        _stageDistance = Vector2.Distance(_stageStart.transform.position, _stageFinish.transform.position);
    }
}
