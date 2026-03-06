using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int spawnCount = 3;
    [SerializeField] private float spawnRadius = 5f;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("EnemySpawner: enemyPrefab is not assigned.");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 circle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(circle.x, 0f, circle.y);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyChase chase = enemy.GetComponent<EnemyChase>();
            if (chase != null && player != null)
            {
                chase.SetTarget(player);
            }
        }
    }
}