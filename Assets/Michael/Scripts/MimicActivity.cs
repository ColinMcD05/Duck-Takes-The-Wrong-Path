using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MimicActivity : MonoBehaviour
{
    private GameObject mimic;
    private Vector3 scaleChange;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            transform.localScale = new Vector3(1, 2, 1);

            if (timer > 5)
            {
                transform.localScale = new Vector3(1, 1, 1);
                timer = 0;
            }
        }
    }
}
