    using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeMovement : MonoBehaviour
{
    private Rigidbody2D ab;
    public float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ab = GetComponent<Rigidbody2D>();
        Vector3 direction = transform.position;
        ab.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
