using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject scoreUI;
    public GameObject coinsUI;
    public GameObject timerUI;
    public GameObject livesUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    private PlayerController playerController;
    private GameManager gameManager;

    int score;
    int coins;
    int lives;
    int time;

    private void Start()
    {   
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0; 
        coins = 0;
        lives = playerController.lives;
        time = 40;
        timerUI.SetActive(true);
        timerText.text = "Timer: " + time;
        scoreUI.SetActive(true);
        scoreText.text = "Score: " + score;
        livesUI.SetActive(true);
        livesText.text = "Lives: " + lives;
        coinsUI.SetActive(true);
        coinsText.text = "Coins: " + coins;
        StartCoroutine(DecreasedTime());
    }

    private void Update()
    {
        score = gameManager.score;
        coins = gameManager.coins;
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        lives = playerController.lives;
        livesText.text = "Lives: " + lives;
    }

    IEnumerator DecreasedTime()
    {
        yield return new WaitForSeconds(1);
        time--;
        timerText.text = "Timer: " + time;
        StartCoroutine(DecreasedTime());
    }
}
