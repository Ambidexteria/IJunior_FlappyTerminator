using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GenericSpawner<Enemy>
{
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private List<Enemy> _enemiesInScene = new();

    private void Start()
    {
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

    public Vector3 GetRandomSpawnPosition()
    {
        return _spawnZone.GetRandomSpawnPosition();
    }

    public override Enemy Spawn()
    {
        Enemy enemy = GetNextObject();
        enemy.transform.position = GetRandomSpawnPosition();
        _enemiesInScene.Add(enemy);
        enemy.Despawning += Despawn;

        return enemy;
    }

    public override void Despawn(Enemy enemy)
    {
        _enemiesInScene.Remove(enemy);
        ReturnToPool(enemy);
    }
}
