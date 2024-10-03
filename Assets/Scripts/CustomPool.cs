using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomPool<Type> where Type : MonoBehaviour
{
    private Queue<Type> _pooledObjects;

    private readonly Func<Type> _createFunc;
    private readonly Action<Type> _actionOnGet;
    private readonly Action<Type> _actionOnRelease;
    private readonly Action<Type> _actionOnDestroy;
    private readonly int _maxSize;

    internal bool _collectionCheck;

    public int CountAll { get; private set; }
    public int CountActive => CountAll - CountInactive;
    public int CountInactive => _pooledObjects.Count;

    public CustomPool(Func<Type> createFunc, Action<Type> actionOnGet = null, Action<Type> actionOnRelease = null, Action<Type> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
    {
        if (createFunc == null)
        {
            throw new ArgumentNullException("createFunc");
        }

        if (maxSize <= 0)
        {
            throw new ArgumentException("Max Size must be greater than 0", "maxSize");
        }

        _pooledObjects = new();
        _createFunc = createFunc;
        _maxSize = maxSize;
        _actionOnGet = actionOnGet;
        _actionOnRelease = actionOnRelease;
        _actionOnDestroy = actionOnDestroy;
        _collectionCheck = collectionCheck;
    }

    public Type Get()
    {
        Type val;

        if (CountInactive < 3)
        {
            _pooledObjects.Enqueue(_createFunc());
            CountAll++;
        }

        val = _pooledObjects.Dequeue();
        _actionOnGet?.Invoke(val);
        return val;
    }

    public void Return(Type element)
    {
        if (CountInactive >= _maxSize)
        {
            CountAll--;
            _actionOnDestroy?.Invoke(element);
            return;
        }

        _actionOnRelease?.Invoke(element);

        if (_pooledObjects.Contains(element) == false)
            _pooledObjects.Enqueue(element);
    }

    public void Clear()
    {
        if (_actionOnDestroy != null)
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                _actionOnDestroy(_pooledObjects.Dequeue());
            }
        }

        CountAll = 0;
    }

    public void Dispose()
    {
        Clear();
    }
}
