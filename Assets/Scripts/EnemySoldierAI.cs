using UnityEngine;

public class EnemySoldierAI : MonoBehaviour
{
    [Header("Combat")]
    public float detectionDistance = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero; // Soldier stays still
    }

    void Update()
    {
        rb.linearVelocity = Vector2.zero; // Always stay in place

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;

        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist <= detectionDistance)
        {
            FacePlayer(player);

            if (Time.time >= nextFireTime)
            {
                Shoot(player);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FacePlayer(GameObject player)
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    void Shoot(GameObject player)
    {
        Vector2 dir = (player.transform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(dir, gameObject);
    }
}
