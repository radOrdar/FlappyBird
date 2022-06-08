using UnityEngine;

namespace DefaultNamespace
{
    public interface IPool<T> where T : MonoBehaviour, IPooled<T>
    {
        T Get();
        
        void Reclaim(T obj);
    }
}