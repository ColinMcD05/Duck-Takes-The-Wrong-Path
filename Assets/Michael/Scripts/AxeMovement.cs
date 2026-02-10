    using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeMovement : MonoBehaviour
{
    private Rigidbody2D ab;
    public float force;
    public int axeDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ab = GetComponent<Rigidbody2D>();
        ab.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1f * axeDirection, 1f, 0f) * Time.deltaTime * 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDeath>().Death();
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
