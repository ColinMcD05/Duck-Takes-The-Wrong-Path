using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SwitchPower(gameObject.name);
        }
    }
}
