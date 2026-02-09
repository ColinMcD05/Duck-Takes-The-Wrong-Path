using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float maxHeight = 6;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Animator playerAnimator;

    private bool isJumping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
        // Debug.DrawRay(transform.position, Vector2.down, Color.red);
        // Debug.Log(GetIsGrounded());
        playerAnimator.SetBool("IsGrounded", GetIsGrounded());
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.GetComponent<PlayerMovement>().inControl)
        {
            if (isJumping && GetIsGrounded())
            {

                Jump();
            }
        }
    }
    public bool GetIsGrounded()
    {
        float playerHalfHeight = spriteRenderer.bounds.extents.y;
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }

    void Jump()
    {
        playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 0f);
        playerRigidbody.AddForce(Vector2.up * maxHeight, ForceMode2D.Impulse);
    }
}
