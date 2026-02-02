using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;
    private PlayerMovement playerMovement;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = gameObject.GetComponent<PlayerController>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }
    
    public void Death()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        playerController.lives -= 1;
        // If player still has live, restart current level
        if (playerController.lives == 0)
        {
            playerMovement.inControl = false;
            gameManager.Invoke("RestartGame()", 4f);
            Debug.Log(playerController.lives);
        }
        // Else, restart the game
        else
        {
            playerMovement.inControl = false;
            // Must change this, but need to wait till main menu scene is made, make game reset in game manager
            gameManager.Invoke("RestartLevel", 4f);
        }

    }
}
