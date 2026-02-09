using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PowerUps : MonoBehaviour
{

    private float maxHeight;
    private float currentHeight;
    public bool goingUp;
    [SerializeField] Rigidbody2D powerUpRigidbody;
    void Awake()
    {
        maxHeight = 1f;
    }

    void Update()
    {
        if (goingUp)
        {
            MoveUp();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SwitchPower(gameObject.tag);
            Destroy(this.gameObject);
        }
    }

    void MoveUp()
    {
        if (currentHeight <= maxHeight)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 2);
            currentHeight += Time.deltaTime * 2;
        }
        else
        {
            goingUp = false;
            powerUpRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
