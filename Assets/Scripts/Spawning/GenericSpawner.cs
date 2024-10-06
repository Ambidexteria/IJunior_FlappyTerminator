using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSpawner<Type> : MonoBehaviour where Type : SpawnableObject
{
    [SerializeField] private Type _prefab;
    [SerializeField] private int _poolDefaultCapacity = 20;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private List<Type> _spawnedObjects;

    private CustomPool<Type> _pool;
    private int _spawnedObjectsCount;

    public event Action<int> ActiveCountChanged;
    public event Action<int> AllCountChanged;

    private void Awake()
    {
        InitializePool();
    }

    public abstract Type Spawn();

    public abstract void Despawn(Type type);

    public void ReturnToPool(Type spawnedObject)
    {
        PrepareToDeactivate(spawnedObject);
        _pool.Return(spawnedObject);
        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    public Type GetNextObject()
    {
         AllCountChanged?.Invoke(++_spawnedObjectsCount);
        return _pool.Get();
    }

    public virtual void PrepareToDeactivate(Type spawnedObject) { }

    private Type PrepareForSpawn(Type spawnedObject)
    {
        spawnedObject.PrepareForSpawn();
        spawnedObject.gameObject.SetActive(true);
        ActiveCountChanged?.Invoke(_pool.CountActive);

        return spawnedObject;
    }

    private void InitializePool()
    {
        _pool = new CustomPool<Type>(
            createFunc: () => Create(),
            actionOnGet: (spawnedObject) => PrepareForSpawn(spawnedObject),
            actionOnRelease: (spawnedObject) => spawnedObject.gameObject.SetActive(false),
            actionOnDestroy: (spawnedObject) => Destroy(spawnedObject.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize
            );
    }

    private Type Create()
    {
        Type spawnedObject = Instantiate(_prefab);
        _spawnedObjects.Add(spawnedObject);
        return spawnedObject;
    }
}