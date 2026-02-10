using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float waitTime;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        Invoke("RestartGame", waitTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        gameManager.lives = 3;
        gameManager.score = 0;
        gameManager.coins = 0;
        gameManager.level = 0;
        gameManager.playerLastPower = "Small";
    }
}
