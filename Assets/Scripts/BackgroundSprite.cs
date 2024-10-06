using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BackgroundSprite : MonoBehaviour
{
    private Collider2D _collider;

    public float Length => _collider.bounds.size.x;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
}
