using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpButton;
    [SerializeField] private KeyCode _shootButton;

    public event Action Jumped;
    public event Action ShotPerformed;

    private void Update()
    {
        if (Input.GetKeyDown(_jumpButton))
            Jumped?.Invoke();

        if (Input.GetKeyDown(_shootButton))
            ShotPerformed?.Invoke();
    }
}
