using UnityEngine;

public class GunShootProjectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.15f;

    private float nextFireTime;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (firePoint == null || bulletPrefab == null) return;

        Vector2 direction = firePoint.right;
        Vector3 spawnPos = firePoint.position + (Vector3)(direction * 0.4f);

        GameObject b = Instantiate(bulletPrefab, spawnPos, firePoint.rotation);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Init(direction, gameObject);
        }
    }
}
