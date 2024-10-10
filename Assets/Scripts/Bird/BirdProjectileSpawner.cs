using System;
using System.Collections.Generic;
using UnityEngine;

public class BirdProjectileSpawner : GenericSpawner<Projectile>
{
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] private List<Projectile> _projectiles = new List<Projectile>();

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
            foreach (var projectile in _projectiles)
            {
                projectile.Destroyed -= Despawn;
            }
        }
    }

    public override void Despawn(Projectile projectile)
    {
        _projectiles.Remove(projectile);
        projectile.Destroyed -= Despawn;
        ReturnToPool(projectile);
    }

    public override Projectile Spawn()
    {
        Projectile projectile = GetNextObject();
        projectile.SetType(_projectileType);
        projectile.Destroyed += Despawn;
        _projectiles.Add(projectile);

        return projectile;
    }
}
