using UnityEngine;

public class BossController : MonoBehaviour
{
    public int bossLives;
    private bool canMove;
    private float enemyHalfHeight;
    [SerializeField] SpriteRenderer enemySprite;
    [SerializeField] Rigidbody2D enemyRigidbody;

    void Update()
    {
        if (canMove)
        {
            Movement();
            Throw();
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bossLives--;
            Destroy(collision.gameObject, 0.5f);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        }
    }

    void Movement()
    {

    }

    void Throw()
    {

    }

    void Jump()
    {
        if (IsGrounded())
        {

        }
    }

    bool IsGrounded()
    {
        enemyHalfHeight = enemySprite.bounds.extents.y;
        return Physics2D.Raycast(transform.position, Vector2.down, enemyHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }
}
