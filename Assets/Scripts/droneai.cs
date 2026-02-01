using UnityEngine;

public class droneai : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float patrolDistance = 2f;

    [Header("Combat")]
    public float detectionRadius = 5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private Vector3 startPos;
    private bool movingRight = true;
    private Rigidbody2D rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (PlayerInRange())
            AttackPlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        float direction = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (movingRight && transform.position.x >= startPos.x + patrolDistance)
            Flip();

        if (!movingRight && transform.position.x <= startPos.x - patrolDistance)
            Flip();
    }

    void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;

        // 👉 Rotate fire point toward player
        Vector2 dir = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        Shoot(dir);
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionRadius;
    }

    void Shoot(Vector2 dir)
    {
        if (Time.time >= nextFireTime && bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().Init(dir, gameObject);

            nextFireTime = Time.time + fireRate;
        }
    }

    // 🎯 Draw detection radius and ray to player
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
