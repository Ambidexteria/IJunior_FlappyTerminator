using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bird))]
public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _offset = 5f;
    [SerializeField] private float _depth = -5f;

    private Bird _bird;

    private void Start()
    {
        _bird = GetComponent<Bird>();

        if (_mainCamera == null)
            throw new NullReferenceException();
    }

    private void Update()
    {
        Vector3 position = _mainCamera.transform.position;
        position.x = _bird.transform.position.x + _offset;
        _mainCamera.transform.position = position;
    }
}
