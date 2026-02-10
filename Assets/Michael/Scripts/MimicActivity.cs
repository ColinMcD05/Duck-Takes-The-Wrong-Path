using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MimicActivity : MonoBehaviour
{
    private GameObject mimic;
    private Vector3 scaleChange;
    private float timer;
    public bool stoodOn = false;
    public bool isOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stoodOn == false)
        {
            timer -= Time.deltaTime;
            if (!isOpen && timer == 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Open"); 
                Open();
            }
            if (isOpen && timer == 0)
            {
                Close();
            }
        }
    }

    void Open()
    {
        timer = 5f;
        isOpen = true;
    }

    void Close()
    {
        timer = 3f;
        isOpen = false;
    }
}
