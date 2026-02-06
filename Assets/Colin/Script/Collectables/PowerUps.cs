using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PowerUps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SwitchPower(gameObject.tag);
            Destroy(this.gameObject);
        }
    }
}
