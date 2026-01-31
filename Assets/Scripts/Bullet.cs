using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public LayerMask hitLayers;

    private Vector2 dir;

    public void Init(Vector2 direction)
    {
        dir = direction.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

   void OnTriggerEnter2D(Collider2D other)
{
    // Ignore everything except Enemy
    // if (((1 << other.gameObject.layer) & hitLayers) == 0) return;

    EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

    if (enemy != null)
    {
        enemy.TakeDamage(1);
        Destroy(gameObject);
    }
}




}
