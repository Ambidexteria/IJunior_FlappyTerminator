using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private BackgroundSprite[] _sprites;
    [SerializeField] private BackgroundDetector _detectorOutOfView;
    [SerializeField] private BackgroundDetector _detectorCurrent;

    private BackgroundSprite _currentActive;

    private void Awake()
    {
        if (_detectorOutOfView == null || _detectorCurrent == null || _sprites == null)
            throw new NullReferenceException();

        if (_sprites.Length == 0)
            throw new Exception();

        _currentActive = _sprites[0];
    }

    private void OnEnable()
    {
        _detectorOutOfView.Triggered += Replace;  
        _detectorCurrent.Triggered += ChangeCurrentBackground;
    }

    private void OnDisable()
    {
        _detectorOutOfView.Triggered -= Replace;
        _detectorCurrent.Triggered -= ChangeCurrentBackground;
    }

    private void Replace(BackgroundSprite sprite)
    {
        Vector2 position = new Vector2(_currentActive.transform.position.x + _currentActive.Length, _currentActive.transform.position.y);
        sprite.transform.position = position;
    }

    private void ChangeCurrentBackground(BackgroundSprite sprite)
    {
        _currentActive = sprite;
    }
}
