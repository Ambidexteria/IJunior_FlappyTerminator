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
        if (_birdCollisionHandler == null)
            throw new NullReferenceException();

        if (_birdMover == null)
            throw new NullReferenceException();
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
        if (interactable is Projectile) 
        {
            Projectile projectile = (Projectile) interactable;

            if (projectile.Type == ProjectileType.Enemy)
            {
                Dead?.Invoke();
            }
        }

        if (interactable is Enemy || interactable is Ground)
            Dead?.Invoke();

        if(interactable is StageFinish)
            Win?.Invoke();
    }
}
