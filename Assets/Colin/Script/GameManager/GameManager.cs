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
    public AudioClip oneUp;

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

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
    }

    public void AddCoin(int earnedCoin)
    {
        coins += earnedCoin;
        if (coins == 40)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(oneUp, 0.5f);
            lives++;
            coins = 0;
        }
    }
}
