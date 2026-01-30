using UnityEngine;

public class GunAttach : MonoBehaviour
{
    public Transform player;
    public Vector3 rightOffset = new Vector3(0.6f, 0.1f, 0f);
    public Vector3 leftOffset = new Vector3(-0.6f, 0.1f, 0f);

    void LateUpdate()
    {
        if (player == null) return;

        bool facingRight = player.localScale.x > 0;
        transform.localPosition = facingRight ? rightOffset : leftOffset;

        // flip gun sprite by scaling X (optional)
        Vector3 s = transform.localScale;
        s.x = facingRight ? 1f : -1f;
        transform.localScale = s;
    }
}
