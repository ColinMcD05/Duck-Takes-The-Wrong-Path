using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MimicActivity : MonoBehaviour
{
    private GameObject mimic;
    private float timer;
    public bool stoodOn = false;
    public bool isOpen;
    bool canMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stoodOn == false && canMove)
        {
            timer -= Time.deltaTime;
            if (!isOpen && timer <= 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Open"); 
                Open();
            }
            else if (isOpen && timer <= 0)
            {
                Close();
            }
        }
    }

    void Open()
    {
        timer = 4f;
        isOpen = true;
    }

    void Close()
    {
        timer = 5f;
        isOpen = false;
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if  (collision.gameObject.CompareTag("Player"))
        {
            stoodOn = false;
        }
    }
}
