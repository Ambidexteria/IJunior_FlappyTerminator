using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyProjectileSpawner _projectileSpawner;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _cooldown = 5f;

    private WaitForSeconds _wait;

    private void Awake()
    {
        if (_projectilePrefab == null)
            throw new NullReferenceException();

        _wait = new WaitForSeconds(_cooldown);
    }

    private void Start()
    {
        StartCoroutine(LaunchShootCoroutine());
    }

    public void SetProjectileSpawner(EnemyProjectileSpawner spawner)
    {
        _projectileSpawner = spawner;
    }

    private IEnumerator LaunchShootCoroutine()
    {
        while(enabled)
        {
            yield return _wait;
            Shoot();
        }
    }

    private void Shoot()
    {
        Projectile projectile = _projectileSpawner.GetNextObject();
        projectile.transform.position = transform.position;
        projectile.Rigidbody.velocity = _speed * Vector2.left;
    }
}
