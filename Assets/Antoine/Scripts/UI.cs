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
    private UI Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    private PlayerController playerController;
    private GameManager gameManager;

    int score;
    int coins;
    int lives;
    public int time;

    void Awake()
    {
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

    private void Start()
    {   
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0; 
        coins = 0;
        lives = gameManager.lives;
        timerUI.SetActive(true);
        timerText.text = "Timer: " + time;
        scoreUI.SetActive(true);
        scoreText.text = "Score: " + score;
        livesUI.SetActive(true);
        livesText.text = "Lives: " + lives;
        coinsUI.SetActive(true);
        coinsText.text = "Coins: " + coins;
    }

    private void Update()
    {
    if (SceneManager.GetActiveScene().buildIndex == 0)
    {
        Destroy(this.gameObject);
    }
    score = gameManager.score;
        coins = gameManager.coins;
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        lives = gameManager.lives;
        livesText.text = "Lives: " + lives;
        timerText.text = "Timer: " + time;
    }
}
