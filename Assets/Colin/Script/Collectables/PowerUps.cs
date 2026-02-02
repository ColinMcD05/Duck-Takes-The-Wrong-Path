using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public int pointValue;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerController>().currentPower != gameObject.name)
            {
                collision.GetComponent<PlayerPowers>().SwitchPower(gameObject.name);
            }
        }
    }
}
