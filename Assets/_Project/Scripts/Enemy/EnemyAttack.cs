using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackInterval = 1f;

    private Transform target;
    private float timer;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null) return;

        timer += Time.deltaTime;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= attackRange && timer >= attackInterval)
        {
            timer = 0f;

            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}