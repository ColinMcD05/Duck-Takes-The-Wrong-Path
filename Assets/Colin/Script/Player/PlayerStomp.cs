using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStomp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D playerRigidbody;

    private float rayLength = 0.5f;
    private Vector2 playerBottom;

    private void Start()
    {
        playerBottom = new Vector2(transform.position.x ,transform.position.y - spriteRenderer.bounds.extents.y + 0.3f);
    }

    bool Stomp()
    {
        return Physics2D.Raycast(playerBottom, Vector2.down, rayLength, LayerMask.GetMask("Enemy"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (playerRigidbody.linearVelocityY <0)
            {
                Destroy(collision.gameObject);
                // Debug.Log("Destroy Object.");
            }
            else
            {
                gameObject.GetComponent<PlayerDeath>().Death();
            }
        }
    }
}
