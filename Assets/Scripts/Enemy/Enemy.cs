using System;
using UnityEngine;

public class Enemy : SpawnableObject, IInteractable
{
    [SerializeField] private EnemyShooter _shooter;
    [SerializeField] private EnemyCollisionHandler _collisionHandler;
    [SerializeField] private float _speed = 3f;

    public event Action<Enemy> Destroyed;
    public event Action<Enemy> Despawning;

    public EnemyShooter Shooter => _shooter;

    private void Awake()
    {
        if (_shooter == null)
            throw new NullReferenceException();

        if (_collisionHandler == null)
            throw new NullReferenceException();
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
        if (interactable is DespawnZone)
        {
            Despawning?.Invoke(this);
        }

        if (interactable is Projectile)
        {
            Projectile projectile = interactable as Projectile;

            if (projectile.Type == ProjectileType.Bird)
            {
                Despawning?.Invoke(this);
                Destroyed?.Invoke(this);
            }
        }
    }
}
