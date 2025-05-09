using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool<T> where T : MonoBehaviour
{
    Queue<T> pool = new();
    public SimpleObjectPool(int poolSize, T prefab, Action<T> onCreate)
    {
        for(int i = 0; i < poolSize; i++)
        {
            T instance = MonoBehaviour.Instantiate<T>(prefab);
            onCreate(instance);
            pool.Enqueue(instance);
        }
    }

    public T Get()
    {
        if(pool.Count != 0)
        {
            return pool.Dequeue();
        }

        return null;
    }
    public void Return(T obj)
    {
        pool.Enqueue(obj);
    }
}