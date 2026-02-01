using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private bool isDead = false;

    void OnEnable()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int amount)
    {
         Debug.Log("Enemy Hit!");
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
