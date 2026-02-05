using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public int score;
    public int coins;
    public float timer;

    void Update()
    {
        if (player.GetComponent<PlayerMovement>().inControl)
        {
            LowerTime();
        }
    }
    
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
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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

    void LowerTime()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            float timeLeft = Mathf.FloorToInt(timer % 60);
            Debug.Log(timeLeft);
        }
        else
        {
            player.GetComponent<PlayerController>().SwitchPower("");
            player.GetComponent<PlayerDeath>().Death();
        }
    }
}
