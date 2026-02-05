using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private bool dead;
    public int deathMaxHeight;
    public int deathCurrentHeight;
    public float deathTimer;
    private int deathSpeed;
    public int upOrDown;

    private void Awake()
    {
        dead = false;
        deathSpeed = 3;
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = gameObject.GetComponent<PlayerController>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (dead)
        {
            DeathMovement();
        }
    }

    public void Death()
    {
        // If player still has live, restart current level
        if (playerController.currentPower == "Small")
        {
            dead = true;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            if (playerController.lives == 0)
            {
                playerMovement.inControl = false;
                gameManager.Invoke("RestartGame", 4f);
                Debug.Log(playerController.lives);
                playerController.lives -= 1;
            }
            // Else, restart the game
            else
            {
                playerMovement.inControl = false;
                // Must change this, but need to wait till main menu scene is made, make game reset in game manager
                gameManager.Invoke("RestartLevel", 4f);
            }
        }
        else
        {
            gameObject.GetComponent<PlayerPowers>().Shrink();
        }
    }

    void DeathMovement()
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
