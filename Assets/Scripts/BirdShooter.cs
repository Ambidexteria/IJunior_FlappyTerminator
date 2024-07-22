using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private PlayerInput _input;

    private void OnEnable()
    {
        _input.EKeyPressed += Shoot;
    }

    private void OnDisable()
    {
        _input.EKeyPressed -= Shoot;
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(_projectilPrefab, transform.position, transform.rotation);
        projectile.Rigidbody.velocity = _speed * transform.right;
    }
}
