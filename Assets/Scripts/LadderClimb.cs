using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 4f;

    private bool onLadder;
    private float originalGravity;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        if (onLadder)
        {
            float v = Input.GetAxisRaw("Vertical");

            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0f, v * climbSpeed);

            if (v == 0) rb.linearVelocity = new Vector2(0f, 0f);
        }
        else
        {
            rb.gravityScale = originalGravity;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = true;
            // snap player to ladder center so you don't fall out of trigger
            transform.position = new Vector3(other.bounds.center.x, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
            onLadder = false;
    }
}
