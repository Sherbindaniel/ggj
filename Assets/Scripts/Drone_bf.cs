using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public float speed = 3f;
    public float hoverHeight = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;

    float nextFire;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;

        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + hoverHeight, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Time.time >= nextFire)
        {
            Shoot(player);
            nextFire = Time.time + fireRate;
        }
    }

    void Shoot(GameObject player)
    {
        Vector2 dir = (player.transform.position - firePoint.position).normalized;
        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        b.GetComponent<Bullet>().Init(dir, gameObject);
    }
}
