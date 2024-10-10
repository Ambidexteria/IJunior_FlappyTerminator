using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GenericSpawner<Enemy>
{
    [SerializeField] private EnemyProjectileSpawner _projectileSpawner;
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private List<Enemy> _enemiesInScene = new();

    private void Start()
    {
        if (_projectileSpawner == null)
            throw new NullReferenceException();

        if (_spawnZone == null)
            throw new NullReferenceException();
    }

    private void OnEnable()
    {
        if (_enemiesInScene.Count > 0)
        {
            foreach (Enemy enemy in _enemiesInScene)
            {
                enemy.Despawning += Despawn;
            }
        }
    }

    private void OnDisable()
    {
        if (_enemiesInScene.Count > 0)
        {
            foreach (Enemy enemy in _enemiesInScene)
            {
                enemy.Despawning -= Despawn;
            }
        }
    }

    public override Enemy Spawn()
    {
        Enemy enemy = GetNextObject();
        enemy.transform.position = _spawnZone.GetRandomSpawnPosition();
        enemy.Despawning += Despawn;
        enemy.Shooter.SetProjectileSpawner(_projectileSpawner);

        _enemiesInScene.Add(enemy);

        return enemy;
    }

    public override void Despawn(Enemy enemy)
    {
        _enemiesInScene.Remove(enemy);
        enemy.Despawning -= Despawn;
        ReturnToPool(enemy);
    }
}
