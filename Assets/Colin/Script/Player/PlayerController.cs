using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public string currentPower = "Small";
    public bool invincible;
    private GameManager gameManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    public List<Sprite> sprite;
    [SerializeField] Animator playerAnimator;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SwitchPower(gameManager.playerLastPower);
    }

    public void SwitchPower(string gainedPower)
    {
        switch (gainedPower)
        {
            case "Grow":
                if (currentPower == "Small")
                {
                    if (invincible)
                    {
                        ChangeSprite(sprite[4]);
                        playerAnimator.SetInteger("SpriteType", 4);
                    }
                    else
                    {
                        ChangeSprite(sprite[1]);
                        playerAnimator.SetInteger("SpriteType", 1);
                    }
                    currentPower = "Grow";
                    gameObject.GetComponent<PlayerPowers>().Grow();
                    //Debug.Log(currentPower);
                }
                else
                {
                    gameManager.AddScore(100);
                }
                break;
            case "Water":
                if (currentPower != "Water")
                {
                    if (invincible)
                    {
                        ChangeSprite(sprite[4]);
                        playerAnimator.SetInteger("SpriteType", 4);
                    }
                    else
                    {
                        ChangeSprite(sprite[2]);
                        playerAnimator.SetInteger("SpriteType", 2);
                    }
                    //Debug.Log(currentPower);
                    currentPower = "Water";
                }
                else
                {
                    gameManager.AddScore(200);
                }
                break;
            case "Invincible":
                gameObject.GetComponent<PlayerController>().invincible = true;
                gameObject.GetComponent<PlayerPowers>().Invoke("Invincible", 10f);
                if (currentPower == "Small")
                {
                    ChangeSprite(sprite[3]);
                    playerAnimator.SetInteger("SpriteType", 3);
                }
                else
                {
                    ChangeSprite(sprite[4]);
                    playerAnimator.SetInteger("SpriteType", 4);
                }
                break;
            case "Small":
            case null:
                currentPower = "Small";
                ChangeSprite(sprite[0]);
                //Debug.Log(currentPower);
                break;
        }
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
