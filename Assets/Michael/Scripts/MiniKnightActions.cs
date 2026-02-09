using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniKnightActions : MonoBehaviour
{
    public float speed = 2f;
    public int direction = -1;
    public float jumpHeight = 3f;
    public float jumpTimerMin = 1.0f;
    public float jumpTimerMax = 3.0f;
    public float wanderRange = 5f;
    private Vector2 startPosition;
    [SerializeField] private Rigidbody2D mkb;
    [SerializeField] private SpriteRenderer mks;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    private bool isGrounded;
    public GameObject axe;
    public Transform axePos;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halfWidth = mks.bounds.extents.x;
        halfHeight = mks.bounds.extents.y;
        startPosition = transform.position;
        mkb = GetComponent<Rigidbody2D>();
        mks = GetComponent<SpriteRenderer>();
        mks.flipX = direction == -1 ? false : true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.5)
        {
            if (isGrounded)
            {
                jump();
            }

            if (timer > 2)
            {
                timer = 0;
                axeThrow();
            }
        }
        movement.x = speed * direction;
        movement.y = mkb.linearVelocity.y;
        mkb.linearVelocity = movement;
        SetDirection();
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

        if (mkb.linearVelocity.x > 0) {
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                mks.flipX = false;
            }
            else if (!Physics2D.Raycast(rightPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                mks.flipX = false;
            }
        }

        else if (mkb.linearVelocity.x < 0)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                mks.flipX = true;
            }
            else if (!Physics2D.Raycast(leftPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                mks.flipX = true;
            }
        }
        float distance = Vector2.Distance(transform.position, startPosition);
        
        if (distance > wanderRange)
        {
            direction *= -1;
            mks.flipX = !mks.flipX;
        }
    }

    void jump()
    {
        mkb.AddForce(new Vector2(direction, jumpHeight), ForceMode2D.Impulse);
    }

    void axeThrow()
    {
        Instantiate(axe, axePos.position, Quaternion.identity);
    }

}