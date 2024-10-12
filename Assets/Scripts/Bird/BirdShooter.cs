using UnityEngine;
using System;

public class BirdShooter : MonoBehaviour
{
    [SerializeField] private BirdProjectileSpawner _projectileSpawner;
    [SerializeField] private Transform _shootStartPoint;
    [SerializeField] private float _speed;
    [SerializeField] private PlayerInput _input;

    private void Awake()
    {
        if (_projectileSpawner == null)
            throw new NullReferenceException();

        if (_shootStartPoint == null)
            throw new NullReferenceException();

        if (_input == null)
            throw new NullReferenceException();
    }

    private void OnEnable()
    {
        _input.ShotPerformed += Shoot;
    }

    private void OnDisable()
    {
        _input.ShotPerformed -= Shoot;
    }

    private void Shoot()
    {
        Projectile projectile = _projectileSpawner.Spawn();
        projectile.transform.position = _shootStartPoint.position;
        projectile.Rigidbody.velocity = _speed * transform.right;
    }
}
