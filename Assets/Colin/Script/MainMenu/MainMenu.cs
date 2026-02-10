using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        Movement();
        Teleport();
    }

    private void Movement()
    {
        transform.Translate(-1f * Time.deltaTime * 5f, 0, 0);
    }

    private void Teleport()
    {
        if (transform.position.x <= -34.82)
        {
            transform.position = new Vector2(16f, -2.29908f);
        }
    }
}
