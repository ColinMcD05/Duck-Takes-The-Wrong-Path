using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ChestCrate : MonoBehaviour
{
    public string itemType;
    public bool used;
    public bool move;
    private float moveHeight;
    private float currentPosition = 0;
    private int moveDirection;
    private float origin;
    private bool empty;
    private PlayerController playerController;
    [SerializeField] GameObject wormPrefab;
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> chestSprites;
    [SerializeField] GameObject invinciblePrefab;
    [SerializeField] Animator chestAnimator;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        moveHeight = 0.325f;
        origin = gameObject.transform.position.y;
        moveDirection = 1;
        // Debug.Log(origin);
    }

    void Update()
    {
        if (move)
        {
            Move();
        }
    }

    public void SpawnItem()
    {
        string playerCurrentPower = playerController.currentPower;
        if (!empty)
        {
            if (itemType == "PowerUp")
            {
                if (playerCurrentPower == "Small")
                {
                    GameObject powerup = Instantiate(wormPrefab, transform.position, Quaternion.identity);
                    powerup.GetComponent<PowerUps>().goingUp = true;
                }
                else
                {
                    GameObject powerup = Instantiate(waterPrefab, transform.position, Quaternion.identity);
                    powerup.GetComponent<PowerUps>().goingUp = true;
                }
            }
            else if (itemType == "Invincible")
            {
                GameObject powerup = Instantiate(invinciblePrefab, transform.position, Quaternion.identity);
                powerup.GetComponent<PowerUps>().goingUp = true;
            }
            else if (itemType == "Coin")
            {
                GameObject coinSpawned = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                coinSpawned.GetComponent<Coins>().fromCrate = true;
            }
            else
            {
                Break();
            }
        }
        chestAnimator.SetBool("Hit", true);
        empty = true;
    }

    public void Move()
    {
        if (!used)
        {
            if(currentPosition >= moveHeight)
            {
                moveDirection = -1;
            }
            else if(currentPosition < 0)
            {
                move = false;
                moveDirection = 1;
                transform.position = new Vector2(transform.position.x, origin);
                currentPosition = 0;
                if (empty)
                {
                    used = true;
                }
            }
                transform.Translate(new Vector3(0f, 0.1f * moveDirection, 0f) * Time.deltaTime * 15f);
            currentPosition += 0.1f * moveDirection * Time.deltaTime * 15f;
        }
    }

    public void Break()
    {
        chestAnimator.SetTrigger("Break");
        Destroy(this.gameObject, 0.5f);
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
}
