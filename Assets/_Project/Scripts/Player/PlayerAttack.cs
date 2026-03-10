using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack Start Effects")]
    [SerializeField] private AudioClip attackSE;
    [SerializeField] private GameObject attackFX;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackFXLifeTime = 1f;

    [Header("Hit Effects")]
    [SerializeField] private AudioClip hitSE;
    [SerializeField] private GameObject hitFX;
    [SerializeField] private float fxHeight = 0.2f;
    [SerializeField] private float fxForwardOffset = 0.15f;
    [SerializeField] private float hitFXLifeTime = 2f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string attackTriggerName = "Attack";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        PlayAttackMotion();
        PlayAttackSE();
        SpawnAttackFX();

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponentInParent<EnemyHealth>();
            if (enemy == null) continue;
            if (damagedEnemies.Contains(enemy)) continue;

            damagedEnemies.Add(enemy);

            PlayHitSE(hit.transform.position);
            SpawnHitFX(hit, enemy.transform.position);

            enemy.TakeDamage(damage);
        }
    }

    private void PlayAttackMotion()
    {
        if (animator != null)
        {
            animator.SetTrigger(attackTriggerName);
        }
    }

    private void PlayAttackSE()
    {
        if (attackSE != null)
        {
            AudioSource.PlayClipAtPoint(attackSE, transform.position);
        }
    }

    private void SpawnAttackFX()
    {
        if (attackFX == null) return;

        Vector3 spawnPos = transform.position;
        Quaternion spawnRot = transform.rotation;

        if (attackPoint != null)
        {
            spawnPos = attackPoint.position;
            spawnRot = attackPoint.rotation;
        }

        GameObject fx = Instantiate(attackFX, spawnPos, spawnRot);
        Destroy(fx, attackFXLifeTime);
    }

    private void PlayHitSE(Vector3 hitPosition)
    {
        if (hitSE != null)
        {
            AudioSource.PlayClipAtPoint(hitSE, hitPosition);
        }
    }

    private void SpawnHitFX(Collider hit, Vector3 enemyPosition)
    {
        if (hitFX == null) return;

        Vector3 toEnemy = (enemyPosition - transform.position).normalized;
        if (toEnemy.sqrMagnitude < 0.001f)
        {
            toEnemy = transform.forward;
        }

        Vector3 fxPos =
            hit.bounds.center +
            Vector3.up * fxHeight +
            toEnemy * fxForwardOffset;

        Quaternion fxRot = Quaternion.LookRotation(toEnemy);

        GameObject fx = Instantiate(hitFX, fxPos, fxRot);
        Destroy(fx, hitFXLifeTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}