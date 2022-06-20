using UnityEngine;

public interface IPooled<T> where T : MonoBehaviour, IPooled<T>
{
    Pool<T> OriginFactory { get; set; }

    void OnClaim();
}