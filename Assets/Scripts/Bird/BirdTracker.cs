using System;
using UnityEngine;

public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private float _offset = 5f;

    private void Awake()
    {
        if (_bird == null)
            throw new NullReferenceException();
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.x = _bird.transform.position.x + _offset;
        transform.position = position;
    }
}
