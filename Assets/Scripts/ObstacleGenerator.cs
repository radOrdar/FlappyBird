using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Obstacle obstaclePf;
    [SerializeField] private float spawnRate;
    [SerializeField] private float xSpawnPos;
    [SerializeField] private float yMinPos;
    [SerializeField] private float yMaxPos;

    private Pool<Obstacle> obstaclePool;
    private float lastSpawnTime;

    private void Awake()
    {
        obstaclePool = new Pool<Obstacle>(obstaclePf, transform);
    }

    private void Update()
    {
        if (Game.Instance.CurrentGameState == Game.GameState.GameOver) { return; }

        if (Time.time - spawnRate >= lastSpawnTime)
        {
            lastSpawnTime = Time.time;
            float yPos = Random.Range(yMinPos, yMaxPos);
            Obstacle obstacle = obstaclePool.Get();
            obstacle.Init(new Vector3(xSpawnPos, yPos, 0));
        }
    }
}