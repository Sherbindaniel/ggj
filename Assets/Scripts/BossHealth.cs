using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private bool isDead = false;
    private SpriteRenderer sr;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Boss HP: " + currentHealth);

        StartCoroutine(Flash());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    void Die()
    {
        isDead = true;

        GetComponent<BossAI>().enabled = false;

        Debug.Log("Boss Defeated!");

        Destroy(gameObject, 0.5f);
    }
}
