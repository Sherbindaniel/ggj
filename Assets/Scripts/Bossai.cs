using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Combat")]
    public float detectionDistance = 10f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;
    private Transform player;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        if (PlayerInRange())
            AttackPlayer();
    }

    bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionDistance;
    }

    void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;

        // Face player
        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();

        Vector2 dir = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        Shoot(dir);
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

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
