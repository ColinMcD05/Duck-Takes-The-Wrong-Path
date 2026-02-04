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


    void Start()
    {
        leftClamp = (-Camera.main.orthographicSize * Camera.main.aspect) + 0.5f - Camera.main.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (inControl)
        {
            GetInput();
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void GetInput()
    {
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

    
}
