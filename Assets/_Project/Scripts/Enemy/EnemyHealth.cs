using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int hp = 1;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}