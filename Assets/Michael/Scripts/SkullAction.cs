using UnityEngine;

public class SkullAction : MonoBehaviour
{
    public bool isKicked;
    public int direction;
    public float speed;

    private void Update()
    {
        if (isKicked)
        {
            Movement();
        }
    }

    private void Movement()
    {
        transform.Translate(new Vector2(1f * direction, 0f) * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isKicked)
        {
            if (collision.gameObject.layer == 7 && !collision.gameObject.CompareTag("Boss"))
            {
                collision.gameObject.GetComponent<EnemyDeath>().GetShot();
                collision.gameObject.GetComponent<EnemyDeath>().direction = direction;
            }
            else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Mimic"))
            {
                direction *= -1;
            }
        }
    }
}
