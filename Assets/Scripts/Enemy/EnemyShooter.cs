using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _cooldown = 3f;

    private EnemyProjectileSpawner _projectileSpawner;
    private WaitForSeconds _wait;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        if (_projectilePrefab == null)
            throw new NullReferenceException();

        _wait = new WaitForSeconds(_cooldown);
    }

    private void OnEnable()
    {
        if (_shootCoroutine == null)
        {
            _shootCoroutine = StartCoroutine(LaunchShootCoroutine());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_shootCoroutine);
        _shootCoroutine = null;
    }

    private void Start()
    {
        _shootCoroutine = StartCoroutine(LaunchShootCoroutine());
    }

    public void SetProjectileSpawner(EnemyProjectileSpawner spawner)
    {
        _projectileSpawner = spawner;
    }

    public IEnumerator LaunchShootCoroutine()
    {
        while (enabled)
        {
            yield return _wait;
            Shoot();
        }

        _shootCoroutine = null;
    }

    private void Shoot()
    {
        Projectile projectile = _projectileSpawner.Spawn();
        projectile.transform.position = transform.position;
        projectile.Rigidbody.velocity = _speed * Vector2.left;
    }
}
