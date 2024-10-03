using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;
    [SerializeField] private BirdMover _birdMover;

    public event Action Dead;
    public event Action Win;

    private void Awake()
    {
        if (_birdCollisionHandler == null || _birdMover == null)
            throw new Exception();
    }

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

        if(interactable is StageFinish)
            Win?.Invoke();
    }
}
