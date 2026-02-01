using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D playerRigidbody;
    public float playerSpeed;
    public int playerDirection = 1;
    public float hMovement;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float leftClamp;
    

    // Update is called once per frame
    void Update()
    {
        GetInput();
        leftClamp = -Camera.main.orthographicSize;
        Debug.Log(leftClamp);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void GetInput()
    {
        hMovement = Input.GetAxis("Horizontal");
    }

    void Movement()
    {
        playerRigidbody.linearVelocityX = hMovement * playerSpeed;
        spriteRenderer.flipX = playerRigidbody.linearVelocity.x < 0f;
        if (transform.position.x <= leftClamp)
        {
            transform.position = new Vector2(leftClamp, transform.position.y);
        }
    }
}
