using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkullScript : MonoBehaviour
{
    private bool isKicked;
    public float speed = 2f;
    public int direction = -1;
    [SerializeField] private Rigidbody2D sb;
    [SerializeField] private SpriteRenderer ss;
    private float halfWidth;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isKicked)
        {
            movement.x = speed * direction;
            movement.y = sb.linearVelocity.y;
            sb.linearVelocity = movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
            ss.flipX = !ss.flipX;
        }

        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<EnemyDeath>().GetShot();
        }
    }
}
