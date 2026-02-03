using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public int score;
    public int coins;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.GetComponent<PlayerMovement>().inControl = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.GetComponent<PlayerController>().lives = 3;
        player.GetComponent<PlayerMovement>().inControl = true;
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
    }

    public void AddCoin(int earnedCoin)
    {
        coins += earnedCoin;
        if (coins == 100)
        {
            player.GetComponent<PlayerController>().lives++;
            coins = 0;
        }
    }
}
