using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public int lives = 3;
    public string currentPower = "Small";
    public bool invincible;
    private GameManager gameManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    public List<Sprite> sprite;


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
                if (currentPower == "Small")
                {
                    currentPower = "Grow";
                    gameObject.GetComponent<PlayerPowers>().Grow();
                    Debug.Log(currentPower);
                    ChangeSprite(sprite[1]);
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
                    ChangeSprite(sprite[2]);
                }
                else
                {
                    gameManager.AddScore(200);
                }
                break;
            case "Invincible":
                gameObject.GetComponent<PlayerController>().invincible = true;
                gameObject.GetComponent<PlayerPowers>().Invoke("Invincible", 10f);
                Debug.Log(currentPower);
                break;
            case null:
                currentPower = "Small";
                ChangeSprite(sprite[0]);
                Debug.Log(currentPower);
                break;
        }
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
