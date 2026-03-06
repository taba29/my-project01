using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int remainingEnemies;

    void Start()
    {
        UpdateEnemyCount();
    }

    void Update()
    {
        if (remainingEnemies > 0)
        {
            UpdateEnemyCount();

            if (remainingEnemies == 0)
            {
                OnGameClear();
            }
        }
    }

    void UpdateEnemyCount()
    {
        remainingEnemies = FindObjectsByType<EnemyChase>(FindObjectsSortMode.None).Length;
    }

    void OnGameClear()
    {
        Debug.Log("Game Clear!");
    }
}