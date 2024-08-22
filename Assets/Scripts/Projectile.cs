using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour, IInteractable
{
    [SerializeField] private float _lifeTime = 10f;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private float _timeToDie;

    public Rigidbody2D Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void Start()
    {
        _timeToDie = Time.time + _lifeTime;
    }

    private void Update()
    {
        if (Time.time > _timeToDie)
            Destroy(gameObject);
    }
}
