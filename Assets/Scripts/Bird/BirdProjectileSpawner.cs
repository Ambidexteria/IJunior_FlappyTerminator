using System;
using System.Collections.Generic;
using UnityEngine;

public class BirdProjectileSpawner : GenericSpawner<BirdProjectile>
{
    private List<BirdProjectile> _projectiles = new List<BirdProjectile>();

    private void OnEnable()
    {
        if(_projectiles.Count > 0)
        {
            foreach (var projectile in _projectiles)
            {
                projectile.Destroyed += Despawn;
            }
        }
    }

    private void OnDisable()
    {
        if (_projectiles.Count > 0)
        {
            foreach (var item in _projectiles)
            {
                item.Destroyed -= Despawn;
            }
        }
    }

    public override void Despawn(BirdProjectile type)
    {
        _projectiles.Remove(type);
        ReturnToPool(type);
    }

    public override BirdProjectile Spawn()
    {
        return GetNextObject();
    }
}
