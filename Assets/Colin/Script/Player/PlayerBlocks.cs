using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBlocks : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HittingBlock() && playerRigidbody.linearVelocity.y > 0)
        {
            if (playerController.currentPower != "Small")
            {
                // Move block up and then back down, do not destroy
                collision.gameObject.GetComponent<ChestCrate>().MoveChest();
            }
            else
            {

            }
        }
    }

    private bool HittingBlock()
    {
        float playerHalfHeight = spriteRenderer.bounds.extents.y;
        return Physics2D.Raycast(transform.position, Vector2.up, playerHalfHeight + 0.01f, LayerMask.GetMask("Ground"));
    }
}
