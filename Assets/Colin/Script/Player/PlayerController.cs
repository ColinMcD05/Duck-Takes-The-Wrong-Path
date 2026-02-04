using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public int lives = 3;
    public string currentPower;
    public bool invincible;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SwitchPower(string gainedPower)
    {
        switch (gainedPower)
        {
            case "Grow":
                if (currentPower == "Defualt")
                {
                    currentPower = "Grow";
                    Debug.Log(currentPower);
                }
                else
                {
                    gameManager.AddScore(100);
                }
                break;
            case "Water":
                if (currentPower != "Water")
                {
                    currentPower = "Water";
                    Debug.Log(currentPower);
                }
                else
                {
                    gameManager.AddScore(200);
                }
                break;
            case "Invincible":
                invincible = true;
                Debug.Log(currentPower);
                break;
            case null:
                currentPower = "Defualt";
                Debug.Log(currentPower);
                break;
        }
    }
}
