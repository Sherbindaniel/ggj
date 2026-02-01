using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    private Vector2 dir;
    private GameObject owner;

    public void Init(Vector2 direction, GameObject shooter)
    {
        dir = direction.normalized;
        owner = shooter;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore shooter
        if (other.gameObject == owner) return;

        // If bullet is from PLAYER → damage ENEMY only
        if(owner==null)
            return;
        if (owner.CompareTag("Player"))
        {
            EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);
                return;
            }
        }

        // If bullet is from ENEMY → damage PLAYER only
        if (owner.CompareTag("Enemy"))
        {
            PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                Destroy(gameObject);
                return;
            }
        }

        // Destroy on any surface hit
        Destroy(gameObject);
    }
}

// using UnityEngine.SceneManagement;
// public class LoadNextLevel
// {
//     public int levelIndex;

//     OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             SceneManager.LoadScene(levelIndex);
//         }
//     }
// }
