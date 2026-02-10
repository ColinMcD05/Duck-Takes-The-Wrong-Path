using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerDeath : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private UI gameUI;
    private bool dead;
    public int deathMaxHeight;
    public int deathCurrentHeight;
    public float deathTimer;
    private int deathSpeed;
    public int upOrDown;
    [SerializeField] Animator playerAnimator;
    public float timer;

    private void Awake()
    {
        dead = false;
        deathSpeed = 3;
        timer = 200;
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = gameObject.GetComponent<PlayerController>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        gameUI = GameObject.Find("UIManager").GetComponent<UI>();
    }

    private void Update()
    {
        if (dead)
        {
            DeathMovement();
        }

        if (playerMovement.inControl)
        {
            LowerTime();
        }
    }

    public void Death()
    {
        if (playerMovement.inControl)
        {
            // If player still has live, restart current level
            if (playerController.currentPower == "Small")
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocityY = 0;
                dead = true;
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Animator>().enabled = false;
                gameManager.lives -= 1;
                playerController.ChangeSprite(playerController.sprite[5]);
                if (gameManager.lives == 0)
                {
                    playerMovement.inControl = false;
                    Invoke("RestartGame", 4f);
                    // Debug.Log(gameManager.lives);
                }
                // Else, restart the game
                else
                {
                    playerMovement.inControl = false;
                    // Must change this, but need to wait till main menu scene is made, make game reset in game manager
                    Invoke("RestartLevel", 4f);
                }
            }
            else
            {
                gameObject.GetComponent<PlayerPowers>().Shrink();
            }
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

    void LowerTime()
    {

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            int timeLeft = Mathf.FloorToInt(timer);
            gameUI.time = timeLeft;
            // Debug.Log(timeLeft);
        }
        else
        {
            playerController.SwitchPower("Small");
            Death();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.playerLastPower = "Small";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(3);
    }
}
