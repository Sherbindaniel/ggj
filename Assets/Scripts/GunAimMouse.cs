using UnityEngine;

public class GunAimMouse : MonoBehaviour
{
    public Camera cam;
    public Transform playerRoot;
    public SpriteRenderer gunSprite;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (playerRoot == null) playerRoot = transform.parent;
    }

    void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // Flip player based on mouse side
        bool mouseOnRight = mouseWorld.x >= playerRoot.position.x;
        //playerRoot.localScale = new Vector3(mouseOnRight ? 1 : -1, 1, 1);

        // Aim pivot toward mouse
        Vector3 dir = mouseWorld - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip sprite if aiming left
        if (gunSprite != null)
            gunSprite.flipY = !mouseOnRight;
    }
}
