using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D cameraRigidbody;
    private float bottom;
    private float top;
    private float topClamp;
    private float bottomClamp;
    public bool inControl;

    private void Awake()
    {
        inControl = true;
        bottom = player.transform.position.y;
        top = 16.23974f;
        topClamp = Camera.main.orthographicSize * 0.75f;
        bottomClamp = transform.position.y - 2;
    }

    private void LateUpdate()
    {
        if (inControl)
        {
            if (!player.GetComponent<PlayerMovement>().playerDirection)
            {
                if (player.GetComponent<PlayerMovement>().playerRigidbody.position.x >= transform.position.x - 0.5f)
                {
                    cameraRigidbody.position += new Vector2(player.GetComponent<PlayerMovement>().hMovement, 0f) * Time.deltaTime * (player.GetComponent<PlayerMovement>().playerSpeed);
                    player.GetComponent<PlayerMovement>().leftClamp += player.GetComponent<PlayerMovement>().hMovement * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed;
                }
                else if (player.GetComponent<PlayerMovement>().playerRigidbody.position.x > (transform.position.x - 2) && player.GetComponent<PlayerMovement>().playerRigidbody.position.x < transform.position.x)
                {
                    cameraRigidbody.position += new Vector2(player.GetComponent<PlayerMovement>().hMovement, 0f) * Time.deltaTime * (player.GetComponent<PlayerMovement>().playerSpeed / 1.5f);
                    player.GetComponent<PlayerMovement>().leftClamp += player.GetComponent<PlayerMovement>().hMovement * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed / 1.5f;
                }
            }

            if (player.GetComponent<PlayerMovement>().playerRigidbody.position.y >= topClamp && transform.position.y <= top)
            {
                cameraRigidbody.position += new Vector2(0f, 1f) * Time.deltaTime * (player.GetComponent<Rigidbody2D>().linearVelocityY);
                topClamp += 1f * Time.deltaTime * (player.GetComponent<Rigidbody2D>().linearVelocityY);
                bottomClamp += 1f * Time.deltaTime * (player.GetComponent<Rigidbody2D>().linearVelocityY);
            }
            else if (player.GetComponent<PlayerMovement>().playerRigidbody.position.y <= bottomClamp && transform.position.y >= bottom)
            {
                cameraRigidbody.position += new Vector2(0f, 1f) * Time.deltaTime * (player.GetComponent<Rigidbody2D>().linearVelocityY);
                topClamp += 1f * Time.deltaTime * (player.GetComponent<Rigidbody2D>().linearVelocityY);
                bottomClamp += 1f * Time.deltaTime * (player.GetComponent<Rigidbody2D>().linearVelocityY);
            }
        }
    }
}
