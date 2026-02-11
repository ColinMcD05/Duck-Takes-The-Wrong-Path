using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SwitchPower("Small");
            collision.gameObject.GetComponent<PlayerDeath>().Death();
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
