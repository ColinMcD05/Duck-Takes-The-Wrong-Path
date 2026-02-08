using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerPowers : MonoBehaviour
{
    public GameObject waterBulletPrefab;
    public int bulletDirection;
    public int amountOfWater;
    public float invincibleTimer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] Animator playerAnimator;


    private void Update()
    {
        if (gameObject.GetComponent<PlayerController>().currentPower == "Water" && gameObject.GetComponent<PlayerMovement>().inControl)
        {
            WaterPower();
        }
    }

    public void Grow()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        Debug.Log(transform.position.y);
    }

    public void Shrink()
    {
        // Change Sprite to small and allow grow to spawn in
        if (playerController.currentPower == "Water")
        {
            playerController.currentPower = "Grow";
            playerController.ChangeSprite(playerController.sprite[1]);
            playerAnimator.SetInteger("SpriteType", 1);
            // Debug.Log(playerController.currentPower);
        }
        else if (playerController.currentPower == "Grow")
        {
            playerController.currentPower = "Small";
            playerController.ChangeSprite(playerController.sprite[0]);
            playerAnimator.SetInteger("SpriteType", 0);
            // Debug.Log(playerController.currentPower);
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
    }

    void WaterPower()
    {
        if (gameObject.GetComponent<PlayerMovement>().playerDirection)
        {
            bulletDirection = -1;
        }
        else
        {
            bulletDirection = 1;
        }
        if (Input.GetButtonDown("Fire1") && amountOfWater < 2 )
        {
            Instantiate(waterBulletPrefab, transform.position + new Vector3(0.65f * bulletDirection, 0.12f, 0), Quaternion.identity);
            amountOfWater += 1;
            playerAnimator.SetTrigger("WaterPower");
        }
    }

    public void Invincible()
    {
        playerController.invincible = false;
        switch (playerController.currentPower)
        {
            case "Grow":
                playerController.ChangeSprite(playerController.sprite[1]);
                playerAnimator.SetInteger("SpriteType", 1);
                break;
            case "Water":
                playerController.ChangeSprite(playerController.sprite[2]);
                playerAnimator.SetInteger("SpriteType", 2);
                break;
            case "Small":
                playerController.ChangeSprite(playerController.sprite[0]);
                playerAnimator.SetInteger("SpriteType", 0);
                break;
        }
    }
}
