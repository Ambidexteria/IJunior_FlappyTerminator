using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action SpaceKeyPressed;
    public event Action EKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpaceKeyPressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            EKeyPressed?.Invoke();
    }
}
