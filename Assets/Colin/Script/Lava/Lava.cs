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
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
