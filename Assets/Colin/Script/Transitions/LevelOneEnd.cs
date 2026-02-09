using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelOneEnd : MonoBehaviour
{

    public GameObject moat;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().inControl = false;
            if (collision.gameObject.GetComponent<PlayerJump>().GetIsGrounded())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                TransitionMovement();
            }
            Destroy(GameObject.Find("Enemies"));
        }
    }

    void TransitionMovement()
    {

    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
