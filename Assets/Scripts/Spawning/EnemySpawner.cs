using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GenericSpawner<Enemy>
{
    [SerializeField] private List<Enemy> _enemiesInScene = new();

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

    public override void Spawn()
    {
        Enemy enemy = GetNextObject();
        enemy.transform.position = GetRandomSpawnPosition();
        _enemiesInScene.Add(enemy);
        enemy.Despawning += Despawn;
    }

    public override void Despawn(Enemy enemy)
    {
        _enemiesInScene.Remove(enemy);
        Debug.Log($"Return to pool {enemy.gameObject.name}");
        ReturnToPool(enemy);
    }
}
