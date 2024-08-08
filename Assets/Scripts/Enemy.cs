using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnableObject, IInteractable
{
    [SerializeField] private EnemyCollisionHandler _collisionHandler;

    public override void PrepareForSpawn()
    {
    }

    private void ProcessCollision(IInteractable interactable)
    {
        throw new NotImplementedException();

    }
}
