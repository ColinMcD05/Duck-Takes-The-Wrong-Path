using UnityEngine;

public class Knife : MonoBehaviour
{

    public int bulletDirection;

    void Update()
    {
        transform.Translate(new Vector3(1f * bulletDirection, 1f, 0f) * Time.deltaTime * 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().inControl)
            {
                collision.gameObject.GetComponent<PlayerDeath>().Death();
            }
            Destroy(this.gameObject);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
