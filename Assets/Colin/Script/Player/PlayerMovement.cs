using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public Rigidbody2D playerRigidbody;
    public float playerSpeed;
    public bool playerDirection = false;
    public float hMovement;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float leftClamp;
    public bool inControl = true;
    [SerializeField] Animator playerAnimator;

    void Start()
    {
        leftClamp = (-Camera.main.orthographicSize * Camera.main.aspect) + 0.5f + Camera.main.transform.position.x;
    }

    void Update()
    {
        if (inControl)
        {
            GetInput();
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnimator.SetTrigger("Quack");
            inControl = false;
            Invoke("GainControl", 0.8f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void GetInput()
    {
        bool isWalking;
        hMovement = Input.GetAxis("Horizontal");
        // Player is facing and moving right
        if (hMovement > 0)
        {
            playerDirection = false;
        }
        // Player is facing and moving left
        else if (hMovement < 0)
        {
            playerDirection = true;
        }
        if (hMovement !=0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        playerAnimator.SetBool("IsWalking", isWalking);
    }

    void Movement()
    {
        playerRigidbody.position += new Vector2(hMovement, 0f) * Time.deltaTime * playerSpeed;
        spriteRenderer.flipX = playerDirection;
        if (transform.position.x <= leftClamp)
        {
            transform.position = new Vector2(leftClamp, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    void GainControl()
    {
        inControl = true;
    }
}
