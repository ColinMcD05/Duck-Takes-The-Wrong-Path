using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterMovement : MonoBehaviour
{
    private PlayerPowers player;
    private int bulletDirection;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float bulletSpeed;
    private void Awake()
    {
        bulletSpeed = 10f;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerPowers>();
        bulletDirection = player.bulletDirection;
        if (bulletDirection == -1 )
        {
            spriteRenderer.flipX = true;
        }
    }
    void Update()
    {
        Movement();
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
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Enemy") || collision.CompareTag("Skeleton"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
