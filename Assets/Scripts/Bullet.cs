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
        if (other.gameObject == owner) return;

        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);

            // Prevent multiple hits
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}
