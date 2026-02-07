using UnityEngine;

public class WormMovement : MonoBehaviour
{
    [SerializeField] PowerUps powerUps;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D wormRigidbody;
    public float speed;
    private int wormDirection;

    void Awake()
    {
        wormDirection = 1;
    }

    void Update()
    {
        if (!powerUps.goingUp)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (wormDirection == 1)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        transform.Translate(new Vector2(1f, 0f) * Time.deltaTime * wormDirection * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && GetSide())
        {
            wormDirection *= -1;
        }

        else if (collision.gameObject.layer == 7)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    private bool GetSide()
    {
        float playerHalfWidth = spriteRenderer.bounds.extents.x;
        return Physics2D.Raycast(transform.position, new Vector2(1*wormDirection,0), playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"));
    }
}
