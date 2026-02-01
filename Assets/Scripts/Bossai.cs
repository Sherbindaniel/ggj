using UnityEngine;

public class Bossai : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float patrolDistance = 5f;

    [Header("Combat")]
    public float detectionDistance = 10f;
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
        rb.WakeUp();

       if (PlayerInRange())
            AttackPlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        float direction = movingRight ? 1 : -1;

        // ✔ Correct velocity usage
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (movingRight && transform.position.x >= startPos.x + patrolDistance)
            Flip();

        if (!movingRight && transform.position.x <= startPos.x - patrolDistance)
            Flip();
    }

void AttackPlayer()
{
    rb.linearVelocity = Vector2.zero;

    // 🔥 FACE THE PLAYER
    if (player.position.x > transform.position.x && !movingRight)
        Flip();
    else if (player.position.x < transform.position.x && movingRight)
        Flip();

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

      bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionDistance;
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

     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        if (player)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
