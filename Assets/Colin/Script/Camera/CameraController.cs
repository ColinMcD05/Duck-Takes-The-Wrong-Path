using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    public float leftClamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftClamp = -Camera.main.orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
