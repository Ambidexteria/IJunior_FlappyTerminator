using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : SpawnableObject, IInteractable
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private float _timeToDie;
    private ProjectileType _projectileType;

    public event Action<Projectile> Destroyed;

    public Rigidbody2D Rigidbody => _rigidbody;
    public ProjectileType Type => _projectileType;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DespawnZone zone))
            Destroyed?.Invoke(this);
    }

    public void SetType(ProjectileType type)
    {
        _projectileType = type;
    }
}
