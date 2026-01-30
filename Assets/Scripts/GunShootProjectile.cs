using UnityEngine;

public class GunShootProjectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 0.15f;
    public LayerMask hitLayers;

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

        Vector2 direction = transform.right; // because your gun aims by rotating pivot

        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.hitLayers = hitLayers;
        bullet.Init(direction);
    }
}

