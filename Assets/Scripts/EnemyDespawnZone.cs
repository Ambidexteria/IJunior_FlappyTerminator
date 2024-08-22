using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDespawnZone : MonoBehaviour, IInteractable
{
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }
}
