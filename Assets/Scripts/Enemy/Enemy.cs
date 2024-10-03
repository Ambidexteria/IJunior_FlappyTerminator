using System;
using UnityEngine;

public class Enemy : SpawnableObject, IInteractable
{
    [SerializeField] private EnemyCollisionHandler _collisionHandler;
    [SerializeField] private float _speed = 3f;

    public event Action<Enemy> Destroyed;
    public event Action<Enemy> Despawning;

    private void Awake()
    {
        if (_collisionHandler == null)
            throw new Exception();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector2.left);
    }

    public override void PrepareForSpawn() { }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is EnemyDespawnZone)
        {
            Despawning?.Invoke(this);
        }

        if (interactable is BirdProjectile)
        {
            Despawning?.Invoke(this);
            Destroyed?.Invoke(this);
        }
    }
}
