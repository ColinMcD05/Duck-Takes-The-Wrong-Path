using UnityEngine;

public class Coins : MonoBehaviour
{

    private GameManager gameManager;
    public bool fromCrate;
    public float maxHeight;
    private float currentHeight;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (fromCrate)
        {
            FromCrate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !fromCrate)
        {
            gameManager.AddScore(200);
            gameManager.AddCoin(1);
            Destroy(this.gameObject);
        }
    }

    public void FromCrate()
    {

        if (currentHeight <= maxHeight)
        {
            transform.Translate(Vector2.up * Time.deltaTime *2);
            currentHeight += Time.deltaTime * 2;
        }
        else
        {
            gameManager.AddScore(200);
            gameManager.AddCoin(1);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
