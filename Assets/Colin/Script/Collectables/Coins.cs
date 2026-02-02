using UnityEngine;

public class Coins : MonoBehaviour
{

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddScore(200);
            gameManager.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
