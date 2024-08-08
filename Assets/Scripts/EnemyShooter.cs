using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyProjectile _projectilePrefab;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _cooldown = 5f;

    private WaitForSeconds _wait;

    private void Start()
    {
        StartCoroutine(LaunchShootCoroutine());
    }

    private IEnumerator LaunchShootCoroutine()
    {
        _wait = new WaitForSeconds(_cooldown);

        while(enabled)
        {
            yield return _wait;
            Shoot();
        }
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        projectile.Rigidbody.velocity = _speed * Vector2.left;
    }
}
