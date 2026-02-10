using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStomp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;
    private GameManager gameManager;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip hit;

    private float rayLength = 0.5f;
    private Vector2 playerBottom;
    private int iFrames;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        playerBottom = new Vector2(transform.position.x ,transform.position.y - spriteRenderer.bounds.extents.y - 0.3f);
    }

    bool Stomp()
    {
        return Physics2D.Raycast(playerBottom, Vector2.down, rayLength, LayerMask.GetMask("Enemy"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 )
        {
            if (gameObject.GetComponent<PlayerController>().invincible)
            {
                Destroy(collision.gameObject);
            }
            else if (playerRigidbody.linearVelocityY < -0.000001 && !collision.gameObject.CompareTag("Boss"))
            {
                switch (collision.gameObject.tag)
                {
                    case "Goblin":
                        collision.gameObject.GetComponent<Animator>().enabled = false;
                        collision.gameObject.GetComponent<EnemyDeath>().Squish();
                        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                        gameManager.AddScore(200);
                        break;
                    case "Skeleton":
                        collision.gameObject.GetComponent<Animator>().SetTrigger("Break");
                        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                        collision.gameObject.GetComponent<EnemyDeath>().dead = true;
                        collision.gameObject.GetComponent<EnemyDeath>().canSpawn = true;
                        Destroy(collision.gameObject, 1f);
                        gameManager.AddScore(300);
                        break;
                    case "MiniKnight":
                        collision.gameObject.GetComponent<EnemyDeath>().GetShot();
                        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                        gameManager.AddScore(500);
                        break;
                    case "Skull":
                        collision.gameObject.GetComponent<SkullAction>().isKicked = !collision.gameObject.GetComponent<SkullAction>().isKicked;
                        Vector3 forward = transform.TransformDirection(transform.position);
                        Vector3 toOther = Vector3.Normalize(transform.position - collision.transform.position);
                        Debug.Log(Vector3.Dot(forward, toOther));
                        if (Vector3.Dot(forward, toOther) >= 0)
                        {
                            collision.gameObject.GetComponent<SkullAction>().direction = 1;
                        }
                        else
                        {
                            collision.gameObject.GetComponent<SkullAction>().direction = -1;
                        }

                        break;
                }
                //Debug.Log("Destroy Object.");
                // Debug.Log(playerRigidbody.linearVelocityY);
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, 0f);
                audioPlayer.PlayOneShot(hit, 0.4f);
                playerRigidbody.linearVelocityY = 3;
            }
            else
            {

                if (collision.gameObject.CompareTag("Boss"))
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    gameObject.GetComponent<PlayerController>().SwitchPower("Small");
                }
                if (collision.gameObject.CompareTag("Skull"))
                {
                    if (!collision.gameObject.GetComponent<SkullAction>().isKicked)
                    {
                        collision.gameObject.GetComponent<SkullAction>().isKicked = true;
                        Vector3 forward = transform.TransformDirection(transform.position);
                        Vector3 toOther = Vector3.Normalize(transform.position - collision.transform.position);
                        if (Vector3.Dot(forward, toOther) >= 0)
                        {
                            collision.gameObject.GetComponent<SkullAction>().direction = 1;
                        }
                        else
                        {
                            collision.gameObject.GetComponent<SkullAction>().direction = -1;
                        }
                    }
                    else
                    {
                        gameObject.GetComponent<PlayerDeath>().Death();
                        collision.gameObject.GetComponent<SkullAction>().isKicked = false;
                    }
                }
                else
                {
                    gameObject.GetComponent<PlayerDeath>().Death();
                }
            }
        }
        else if (collision.gameObject.CompareTag("Mimic"))
        {
            if (collision.gameObject.GetComponent<MimicActivity>().isOpen)
            {
                gameObject.GetComponent<PlayerDeath>().Death();
            }
            else
            {
                collision.gameObject.GetComponent<MimicActivity>().stoodOn = true;
            }
        }
    }
}
