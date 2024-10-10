using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpButton;
    [SerializeField] private KeyCode _shootButton;

    public event Action Jump;
    public event Action Shoot;

    private void Update()
    {
        if (Input.GetKeyDown(_jumpButton))
            Jump?.Invoke();

        if (Input.GetKeyDown(_shootButton))
            Shoot?.Invoke();
    }
}
