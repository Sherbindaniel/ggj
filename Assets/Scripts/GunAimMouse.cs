using UnityEngine;

public class GunAimMouse : MonoBehaviour
{
    public Camera cam;
    public Transform playerRoot;      // drag Player here
    public SpriteRenderer gunSprite;  // drag GunSprite's SpriteRenderer here

    [Header("Hand offsets (local to Player)")]
    public Vector2 rightHandOffset = new Vector2(0.6f, 0.1f);
    public Vector2 leftHandOffset = new Vector2(-0.6f, 0.1f);

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (playerRoot == null) playerRoot = transform.parent;
    }

    void Update()
    {
        if (cam == null || playerRoot == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // Decide left/right based on mouse relative to PLAYER
        bool mouseOnRight = mouseWorld.x >= playerRoot.position.x;

        // Move pivot to the correct hand
        transform.localPosition = mouseOnRight
            ? new Vector3(rightHandOffset.x, rightHandOffset.y, 0f)
            : new Vector3(leftHandOffset.x, leftHandOffset.y, 0f);

        // Aim from the pivot position
        Vector3 dir = mouseWorld - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip sprite so it looks correct when aiming left
        if (gunSprite != null)
            gunSprite.flipY = !mouseOnRight;
        if (Input.GetMouseButtonDown(0))
            Debug.Log("CLICK OK");
    }
}
