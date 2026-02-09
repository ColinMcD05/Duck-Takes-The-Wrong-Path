using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D cameraRigidbody;

    private void LateUpdate()
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
    }
}
