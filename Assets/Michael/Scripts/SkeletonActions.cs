using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkeletonActions : MonoBehaviour
{
    private bool canMove;
    public float speed = 2f;
    public int direction = -1;
    [SerializeField] private Rigidbody2D sb;
    [SerializeField] private SpriteRenderer ss;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halfWidth = ss.bounds.extents.x;
        halfHeight = ss.bounds.extents.y;
        sb = GetComponent<Rigidbody2D>();
        ss = GetComponent<SpriteRenderer>();
        ss.flipX = direction == -1 ? false : true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !gameObject.GetComponent<EnemyDeath>().dead && !GameObject.Find("Player").GetComponent<PlayerDeath>().dead)
        {
            movement.x = speed * direction;
            movement.y = sb.linearVelocity.y;
            sb.linearVelocity = movement;
            SetDirection();
        }

        else
        {
            sb.linearVelocity = new Vector2(0, 0);
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void SetDirection()
    {
        if (!isGrounded) return;


        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;

        if (sb.linearVelocity.x > 0) {
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                ss.flipX = false;
            }
            else if (!Physics2D.Raycast(rightPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                ss.flipX = false;
            }
        }

        else if (sb.linearVelocity.x < 0)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                ss.flipX = true;
            }
            else if (!Physics2D.Raycast(leftPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                ss.flipX = true;
            }
        }       
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && !gameObject.GetComponent<EnemyDeath>().dead)
        {
            direction *= -1;
            ss.flipX = !ss.flipX;
        }
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }
}