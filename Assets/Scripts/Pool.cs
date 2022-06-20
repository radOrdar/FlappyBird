using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, IPooled<T>
{
    private T prefab;
    private Transform parent;

    private Queue<T> queue = new Queue<T>();

    public Pool(T prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public T Get()
    {
        T obj;
        if (queue.Count == 0)
        {
            obj = Object.Instantiate(prefab, parent);
        } else
        {
            obj = queue.Dequeue();
        }

        obj.OriginFactory = this;
        obj.gameObject.SetActive(true);
        obj.OnClaim();
        return obj;
    }

    public void Reclaim(T obj)
    {
        obj.gameObject.SetActive(false);
        queue.Enqueue(obj);
    }
}