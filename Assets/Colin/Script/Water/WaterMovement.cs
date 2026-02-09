using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterMovement : MonoBehaviour
{
    private PlayerPowers player;
    private int bulletDirection;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float bulletSpeed;
    [SerializeField] Animator bulletAnimator;
    public bool moving;

    private void Awake()
    {
        bulletSpeed = 10f;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerPowers>();
        bulletDirection = player.bulletDirection;
        moving = true;
        if (bulletDirection == -1 )
        {
            spriteRenderer.flipX = true;
        }
    }
    void Update()
    {
        if (moving)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.Translate(new Vector3(1 * bulletDirection, 0, 0) * Time.deltaTime * bulletSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        player.amountOfWater --;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject, 0.5f);
            bulletAnimator.SetTrigger("Hit");
            moving = false;
        }
        else if (collision.CompareTag("Enemy") || collision.CompareTag("Skeleton"))
        {
            Destroy(this.gameObject, 0.5f);
            Destroy(collision.gameObject);
            bulletAnimator.SetTrigger("Hit");
            moving = false;
        }
    }
}
