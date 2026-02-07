using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    public float center;

    void Start()
    {
        center = transform.position.x - 0.5f;
    }
    private void LateUpdate()
    {
        if (!player.GetComponent<PlayerMovement>().playerDirection)
        {
            if (player.transform.position.x >= center)
            {
                transform.position = new Vector2(player.transform.position.x - 0.5f,0);
                player.GetComponent<PlayerMovement>().leftClamp += player.GetComponent<PlayerMovement>().hMovement * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed;
                center = player.transform.position.x - 0.5f;
            }
            else if (player.transform.position.x > (center - 2) && player.transform.position.x < center)
            {
                transform.position += new Vector3(player.GetComponent<PlayerMovement>().hMovement, 0f) * Time.deltaTime * (player.GetComponent<PlayerMovement>().playerSpeed / 1.5f);
                player.GetComponent<PlayerMovement>().leftClamp += player.GetComponent<PlayerMovement>().hMovement * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed / 1.5f;
                center += player.GetComponent<PlayerMovement>().hMovement * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed/2;
            }
        }
    }
}
