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

    private float rayLength = 0.5f;
    private Vector2 playerBottom;
    private int iFrames;

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
            if (playerRigidbody.linearVelocityY < -0.000001 && !collision.gameObject.CompareTag("Boss"))
            {
                Destroy(collision.gameObject);
                //Debug.Log("Destroy Object.");
               // Debug.Log(playerRigidbody.linearVelocityY);
            }
            else
            {
                if (gameObject.GetComponent<PlayerController>().invincible)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    if (collision.gameObject.CompareTag("Mimic"))
                    {
                        
                    }
                    else
                    {
                        if (collision.gameObject.CompareTag("Boss"))
                        {
                            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                            gameObject.GetComponent<PlayerController>().currentPower = "Small";
                        }
                        gameObject.GetComponent<PlayerDeath>().Death();
                    }
                }
            }
        }
    }
}
