using UnityEngine;

public class GunAttach : MonoBehaviour
{
    public Transform player;
    public Transform firePoint;

    public Vector3 rightOffset = new Vector3(0.6f, 0.1f, 0f);
    public Vector3 leftOffset  = new Vector3(-0.6f, 0.1f, 0f);

   void LateUpdate()
{
    if (player == null) return;

    bool facingRight = Mathf.Approximately(player.eulerAngles.y, 0f);

    transform.localPosition = facingRight ? rightOffset : leftOffset;

    if (firePoint != null)
        firePoint.localRotation = Quaternion.identity;
}

}
