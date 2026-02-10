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
    [SerializeField] AudioSource bulletAudio;
    [SerializeField] AudioClip hit;

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
        if (collision.gameObject.layer == 6 && !collision.gameObject.CompareTag("Mimic"))
        {
            Destroy(this.gameObject, 0.5f);
            bulletAnimator.SetTrigger("Hit");
            moving = false;
            bulletAudio.PlayOneShot(hit, 0.4f);
        }
        else if (collision.gameObject.layer == 7 && !collision.gameObject.CompareTag("Boss"))
        {
            Destroy(this.gameObject, 0.5f);
            collision.gameObject.GetComponent<EnemyDeath>().GetShot();
            collision.gameObject.GetComponent<EnemyDeath>().direction = bulletDirection;
            bulletAnimator.SetTrigger("Hit");
            moving = false;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            bulletAudio.PlayOneShot(hit, 0.4f);
        }
        else if (collision.gameObject.CompareTag("Mimic"))
        {
            Destroy(this.gameObject, 0.5f);
            collision.gameObject.GetComponent<EnemyDeath>().GetShot();
            collision.gameObject.GetComponent<EnemyDeath>().direction = bulletDirection;
            bulletAnimator.SetTrigger("Hit");
            moving = false;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            bulletAudio.PlayOneShot(hit, 0.4f);
        }
    }
}
