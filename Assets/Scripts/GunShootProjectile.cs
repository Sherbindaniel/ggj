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

        // ALWAYS use the muzzle direction
        Vector2 direction = firePoint.right;

        // Spawn bullet with muzzle rotation
        GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.hitLayers = hitLayers;
            bullet.Init(direction);
        }
    }
}
