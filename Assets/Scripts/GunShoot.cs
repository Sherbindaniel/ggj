using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float fireRate = 0.2f;
    public float range = 20f;
    public LayerMask hitLayers;

    public Transform firePoint; // where the shot comes from

    float nextFireTime = 0f;

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
        Vector2 direction = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(
            firePoint.position,
            direction,
            range,
            hitLayers
        );

        Debug.DrawRay(
            firePoint.position,
            direction * range,
            Color.red,
            0.1f
        );

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            // later: damage, effects, etc
        }
    }
}
