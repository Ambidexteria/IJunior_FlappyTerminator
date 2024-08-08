using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;
    [SerializeField] private BirdMover _birdMover;

    public event Action Dead;

    private void OnEnable()
    {
        _birdCollisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _birdCollisionHandler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is EnemyProjectile || interactable is Enemy || interactable is Ground)
            Dead?.Invoke();
    }
}
