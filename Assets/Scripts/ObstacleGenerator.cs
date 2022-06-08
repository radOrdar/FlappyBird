using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour, IPool<Obstacle> {
    
    [SerializeField] private Obstacle obstaclePf;
    [SerializeField] private float spawnRate;
    [SerializeField] private float xSpawnPos;
    [SerializeField] private float yMinPos;
    [SerializeField] private float yMaxPos;

    private Queue<Obstacle> pool = new Queue<Obstacle>();
    private float lastSpawnTime;

    private void Update() {
        if (Game.Instance.IsGameOver) { return; }
        if (Time.time - spawnRate >= lastSpawnTime) {
            lastSpawnTime = Time.time;
            float yPos = Random.Range(yMinPos, yMaxPos);
            Obstacle obstacle = Get();
            obstacle.transform.position = new Vector3(xSpawnPos, yPos, 0);
        }
    }

    public Obstacle Get()
    {
        Obstacle obstacle;
        if (pool.Count == 0)
        {
            obstacle = Instantiate(obstaclePf);
        } else
        {
            obstacle = pool.Dequeue();
        }

        obstacle.OriginFactory = this;
        obstacle.gameObject.SetActive(true);
        obstacle.OnClaim();
        return obstacle;
    }

    public void Reclaim(Obstacle obj)
    {
       obj.gameObject.SetActive(false);
       pool.Enqueue(obj);
    }
}