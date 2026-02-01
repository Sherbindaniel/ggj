using UnityEngine;

public class EnemySoldierAI : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;

    [Header("Combat")]
    public float detectionDistance = 2f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private bool movingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.WakeUp();

        if (PlayerInSight())
            AttackPlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        float direction = movingRight ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;
        Shoot();
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool PlayerInSight()
    {
        Vector2 dir = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dir,
            detectionDistance,
            LayerMask.GetMask("Player")
        );

        return hit.collider != null;
    }

    void Shoot()
    {
        if (Time.time >= nextFireTime && bulletPrefab && firePoint)
        {
            Vector2 shootDir = movingRight ? Vector2.right : Vector2.left;
            Vector3 spawnPos = firePoint.position + (Vector3)(shootDir * 0.4f);

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            bullet.GetComponent<Bullet>().Init(shootDir, gameObject);

            nextFireTime = Time.time + fireRate;
        }
    }
}