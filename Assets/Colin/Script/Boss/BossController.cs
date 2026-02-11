using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int bossLives;
    public bool canMove;
    private float enemyHalfHeight;
    [SerializeField] SpriteRenderer enemySprite;
    [SerializeField] Rigidbody2D enemyRigidbody;
    [SerializeField] Animator enemyAnimator;
    private GameObject player;
    [SerializeField] GameObject knifePrefab;
    public float maxHeight;
    public float jumpTimer;
    public float speed;
    public float moveTimer;
    private float attackTimer;
    float playerPosition;
    int bossDirection;
    int moveDirection;

    void Awake()
    {
        player = GameObject.Find("Player");
        moveDirection = -1;
        moveTimer = 0.25f;
        attackTimer = 2f;
        bossLives = 5;
        canMove = false;
    }

    void Update()
    {
        if (canMove)
        {
            Jump();
            Throw();
            if (IsGrounded())
            {
                jumpTimer -= Time.deltaTime;
                Movement();
            }
            else
            {
                JumpMovement();
            }
        }
        else
        {
            enemyRigidbody.linearVelocityY = 0;
        }
        ChangeDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bossLives--;
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            collision.gameObject.GetComponent<WaterMovement>().moving = false;
            Destroy(collision.gameObject, 0.3f);
        }
        if (bossLives == 0)
        {
            gameObject.GetComponent<BossDeath>().isDead = true;
            canMove = false;
            enemyAnimator.enabled = false;
            enemyRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Movement()
    {
        if (moveTimer <= 0)
        {
            moveTimer = 0.25f;
            moveDirection *= -1;
        }
        else
        {
            enemyRigidbody.position += new Vector2(0.5f, 0f) * Time.deltaTime * speed/2 * moveDirection;
            moveTimer -= Time.deltaTime;
        }
    }

    void Throw()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            float yAttackPosition = Random.Range(transform.position.y - enemySprite.bounds.extents.y * 0.75f, transform.position.y + enemySprite.bounds.extents.y * 0.75f);
            attackTimer = Random.Range(0.5f, 3f);
            GameObject bullet = Instantiate(knifePrefab, new Vector3(transform.position.x - enemySprite.bounds.extents.x, yAttackPosition), Quaternion.Euler(0f,0f,-45f*bossDirection));
            bullet.GetComponent<Knife>().bulletDirection = bossDirection;
            bullet.GetComponent<SpriteRenderer>().flipX = enemySprite.flipX;
        }
    }

    void Jump()
    {
        if (IsGrounded() && jumpTimer <= 0)
        {
            enemyRigidbody.linearVelocity = new Vector2(enemyRigidbody.linearVelocity.x, 0f);
            enemyRigidbody.AddForce(Vector2.up * maxHeight, ForceMode2D.Impulse);
            jumpTimer = Random.Range(5, 11);
            playerPosition = Vector3.Distance(transform.position, player.transform.position)/2;
        }
    }

    void JumpMovement()
    {
        if (playerPosition >= 0)
        enemyRigidbody.position += new Vector2(1f,0f) * Time.deltaTime * speed * bossDirection;
        playerPosition -= 1 * Time.deltaTime * speed;
    }

    bool IsGrounded()
    {
        enemyHalfHeight = enemySprite.bounds.extents.y;
        return Physics2D.Raycast(transform.position, Vector2.down, enemyHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }

    void ChangeDirection()
    {
        if (CheckDirection() > 0)
        {
            enemySprite.flipX = true;
            bossDirection = 1;
        }
        else
        {
            enemySprite.flipX = false;
            bossDirection = -1;
        }
    }

    float CheckDirection()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 toOther = Vector3.Normalize(player.transform.position - transform.position);
        return Vector3.Dot(forward, toOther);
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }
}
