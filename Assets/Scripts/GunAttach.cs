using UnityEngine;

public class GunAttach : MonoBehaviour
{
    public Transform player;
    public Transform firePoint; // drag your FirePoint here

    public Vector3 rightOffset = new Vector3(0.6f, 0.1f, 0f);
    public Vector3 leftOffset = new Vector3(-0.6f, 0.1f, 0f);

    void LateUpdate()
    {
        if (player == null) return;

        bool facingRight = player.localScale.x > 0;

        // move gun to correct side
        transform.localPosition = facingRight ? rightOffset : leftOffset;

        // flip gun sprite
        Vector3 s = transform.localScale;
        s.x = facingRight ? 1f : -1f;
        transform.localScale = s;

        // OPTIONAL SAFETY: ensure firePoint always points outward
        // If your FirePoint is correctly placed as a child, you can skip this.
        if (firePoint != null)
        {
            // Make firePoint's local rotation always zero (inherits flip cleanly)
            firePoint.localRotation = Quaternion.identity;
        }
    }
}
