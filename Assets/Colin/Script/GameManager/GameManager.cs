using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int score;
    public int coins;
    public string playerLastPower;
    public int lives = 3;
    public int level;

    void Awake()
    {
        playerLastPower = "Small"; 
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            lives = 3;
            score = 0;
            coins = 0;
            level = 0;
            playerLastPower = "Small";
        }
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
            lives++;
            coins = 0;
        }
    }
}
