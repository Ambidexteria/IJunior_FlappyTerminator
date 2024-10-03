using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private float _minRotationAngle = -60f;
    [SerializeField] private float _maxRotationAngle = 60f;
    [SerializeField] private PlayerInput _input;

    private Rigidbody2D _rigidbody;
    private Quaternion _targetRotation;

    private void Awake()
    {
        if (_input == null)
            throw new Exception();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _targetRotation = Quaternion.Euler(0, 0, _minRotationAngle);
    }

    private void Update()
    {
        Move();

        if (transform.rotation != _targetRotation)
            Rotate();
    }

    private void OnEnable()
    {
        _input.SpaceKeyPressed += Jump;
    }

    private void OnDisable()
    {
        _input.SpaceKeyPressed -= Jump;
    }

    private void Jump()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        Quaternion rotation = Quaternion.Euler(0, 0, _maxRotationAngle);
        transform.rotation = rotation;
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
