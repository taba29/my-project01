using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float respawnDelay = 3f;

    private bool isRespawning = false;

    void Update()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        if (enemies.Length == 0 && !isRespawning)
        {
            StartCoroutine(RespawnEnemies());
        }
    }

    IEnumerator RespawnEnemies()
    {
        isRespawning = true;

        Debug.Log("All enemies defeated. Respawn in " + respawnDelay + " sec");

        yield return new WaitForSeconds(respawnDelay);

        if (enemySpawner != null)
        {
            enemySpawner.SpawnEnemies();
        }
        else
        {
            Debug.LogWarning("GameManager: enemySpawner is not assigned.");
        }

        isRespawning = false;
    }
}