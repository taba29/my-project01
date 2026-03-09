using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private GameObject deathFX;

    private Renderer r;
    private Color originalColor;

    void Start()
    {
        r = GetComponent<Renderer>();
        if (r != null)
        {
            originalColor = r.material.color;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Enemy hp = " + hp);

        if (hp <= 0)
        {
            StartCoroutine(DieAfterFlash());
        }
        else
        {
            StartCoroutine(HitFlash());
        }
    }

    IEnumerator HitFlash()
    {
        if (r != null)
        {
            r.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            r.material.color = originalColor;
        }
    }

    IEnumerator DieAfterFlash()
    {
        if (r != null)
        {
            r.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        Die();
    }

    private void Die()
    {
        Debug.Log("Enemy Die called");

        if (ScoreManager.I != null)
        {
            ScoreManager.I.AddScore(100);
        }

        if (deathFX != null)
        {
            Debug.Log("DeathFX Instantiate");
            Instantiate(deathFX, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("deathFX is null");
        }

        Destroy(gameObject);
    }
}