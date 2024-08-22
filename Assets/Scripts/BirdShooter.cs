using UnityEngine;

public class BirdShooter : MonoBehaviour
{
    [SerializeField] private BirdProjectile _projectilePrefab;
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
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        projectile.Rigidbody.velocity = _speed * transform.right;
    }
}
