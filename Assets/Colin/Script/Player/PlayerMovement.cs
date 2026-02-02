using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D playerRigidbody;
    public float playerSpeed;
    public bool playerDirection = false;
    public float hMovement;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float leftClamp;
    public bool inControl = true;
    public int upOrDown;
    public int deathMaxHeight;
    public int deathCurrentHeight;
    public float deathTimer;
    private int deathSpeed;


    void Start()
    {
        leftClamp = (-Camera.main.orthographicSize * Camera.main.aspect) + 0.5f;
        deathSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (inControl)
        {
            GetInput();
            Movement();
        }
        else
        {
            Death();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void GetInput()
    {
        hMovement = Input.GetAxis("Horizontal");
        if (hMovement > 0)
        {
            playerDirection = false;
        }
        else if (hMovement < 0)
        {
            playerDirection = true;
        }
    }

    void Movement()
    {
        transform.Translate(new Vector2(hMovement, 0f) *Time.deltaTime * playerSpeed);
        spriteRenderer.flipX = playerDirection;
        if (transform.position.x <= leftClamp)
        {
            transform.position = new Vector2(leftClamp, transform.position.y);
        }
    }

    void Death()
    {
        if (deathTimer <= 1.5f)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= 1.5f)
            {
                upOrDown = 1;
            }
        }
        else if ((deathTimer >= 1.5f && deathTimer < 3) || (deathTimer >= 3.25f))
        {
            if (deathCurrentHeight <= deathMaxHeight && upOrDown == 1)
            {
                deathCurrentHeight += 1;
                if (deathCurrentHeight >= deathMaxHeight)
                {
                    upOrDown = -1;
                    deathSpeed = 6;
                    deathTimer = 3;
                }
            }
            transform.Translate(Vector2.up * Time.deltaTime * deathSpeed * upOrDown);
        }
        else
        {
            deathTimer += Time.deltaTime;
        }
    }
}
