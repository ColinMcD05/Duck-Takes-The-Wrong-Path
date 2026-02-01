using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D playerRigidbody;
    public float playerSpeed;
    public bool playerDirection = false;
    public float hMovement;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float leftClamp;

    void Start()
    {
        leftClamp = (-Camera.main.orthographicSize * Camera.main.aspect) + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
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
}
