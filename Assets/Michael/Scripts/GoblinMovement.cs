using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoblinMovement : MonoBehaviour
{
    public float speed = 2f;
    public int direction = -1;
    [SerializeField] private Rigidbody2D gb;
    [SerializeField] private SpriteRenderer gs;
    private float halfWidth;
    private Vector2 movement;
    private bool canMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halfWidth = gs.bounds.extents.x;
        gb = GetComponent<Rigidbody2D>();
        gs = GetComponent<SpriteRenderer>();
        gs.flipX = direction == -1 ? false : true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !gameObject.GetComponent<EnemyDeath>().dead && !GameObject.Find("Player").GetComponent<PlayerDeath>().dead)
        {
            movement.x = speed * direction;
            movement.y = gb.linearVelocity.y;
            gb.linearVelocity = movement;
            SetDirection();
        }

        else
        {
            gb.linearVelocity = new Vector2(0, 0);
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    void SetDirection()
    {

        if (gb.linearVelocity.x > 0) {
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                gs.flipX = false;
            }
        }

        else if (gb.linearVelocity.x < 0)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                direction *= -1;
                gs.flipX = true;
            }
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            direction *= -1;
            gs.flipX = !gs.flipX;
        }
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }
}