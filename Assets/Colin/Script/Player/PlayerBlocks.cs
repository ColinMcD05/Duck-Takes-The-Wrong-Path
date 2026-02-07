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
        ChestCrate chest = collision.gameObject.GetComponent<ChestCrate>();
        if (HittingBlock() && playerRigidbody.linearVelocityY == 0 && chest != null)
        {
            Debug.Log("Hit block");
            chest.move = true;
            if (playerController.currentPower != "Small" || collision.gameObject.CompareTag("Chest"))
            {
                chest.SpawnItem();
            }
        }
    }

    private bool HittingBlock()
    {
        float playerHalfHeight = spriteRenderer.bounds.extents.y;
        float playerHalfWidth = spriteRenderer.bounds.extents.x;
        Debug.Log(playerRigidbody.linearVelocityY);
        if (Physics2D.Raycast(transform.position, Vector2.up, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground")) || Physics2D.Raycast(transform.position - new Vector3(playerHalfWidth,0,0), Vector2.up, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground")) || Physics2D.Raycast(transform.position + new Vector3(playerHalfWidth,0,0), Vector2.up, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
