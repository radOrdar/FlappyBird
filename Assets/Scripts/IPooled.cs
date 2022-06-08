using UnityEngine;

namespace DefaultNamespace
{
    public interface IPooled<T> where T : MonoBehaviour, IPooled<T>
    {
        IPool<T> OriginFactory { get; set; }

        void OnClaim();
    }
}