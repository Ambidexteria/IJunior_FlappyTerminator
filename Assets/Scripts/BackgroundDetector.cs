using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BackgroundDetector : MonoBehaviour
{
    public event Action<BackgroundSprite> Triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BackgroundSprite sprite))
        {
            Triggered?.Invoke(sprite);
        }
    }
}
