using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float patrolDistance = 3f;

    [Header("Combat")]
    public float detectionDistance = 5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    private float nextFireTime = 0f;

    private Vector3 startPos;
    private bool movingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, (movingRight ? Vector2.right : Vector2.left) * detectionDistance, Color.red);

        if (PlayerInSight())
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
    RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, detectionDistance, LayerMask.GetMask("Player"));

    return hit.collider != null;
}


    void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;
        Shoot();
    }

   void Shoot()
{
    if (Time.time >= nextFireTime && bulletPrefab && firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 shootDir = movingRight ? Vector2.right : Vector2.left;
        bullet.GetComponent<Bullet>().Init(shootDir);

        nextFireTime = Time.time + fireRate;
    }
}


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 dir = movingRight ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(dir * detectionDistance));
    }
}
