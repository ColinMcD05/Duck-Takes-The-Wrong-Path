using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelOneEnd : MonoBehaviour
{

    public GameObject moat;
    private bool levelEnd;
    private float time;
    [SerializeField] Sprite moatSprite;
    private GameManager gameManager;
    private int moveMoat;
    [SerializeField] AudioClip levelEndAudio;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (levelEnd)
        {
            time += Time.deltaTime;
            if (time >= 0.5f && time <= 0.6f)
            {
                moat.GetComponent<SpriteRenderer>().sprite = moatSprite;
                if (moveMoat == 0)
                {
                    moveMoat += 1;
                }
                if (moveMoat == 1)
                {
                    moveMoat += 1;
                    moat.transform.position -= new Vector3(2f, 0);
                }
            }
            if (time >= 0.7f)
            {
                TransitionMovement();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.playerLastPower = collision.gameObject.GetComponent<PlayerController>().currentPower;
            collision.gameObject.GetComponent<PlayerMovement>().inControl = false;
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
            GameObject.Find("Music").GetComponent<AudioSource>().loop = true;
            GameObject.Find("Music").GetComponent<AudioSource>().PlayOneShot(levelEndAudio, 1f);
            if (collision.gameObject.GetComponent<PlayerJump>().GetIsGrounded())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                levelEnd = true;
                gameObject.GetComponent<Animator>().SetTrigger("Crank");
                collision.gameObject.GetComponent<Animator>().SetBool("IsWalking", false);
                collision.gameObject.GetComponent<Animator>().SetTrigger("Crank");
            }
            Destroy(GameObject.Find("Enemies"));
           // Invoke("NextLevel", 5f);
        }
    }

    void TransitionMovement()
    {
        if (GameObject.Find("Player").transform.position.x < 84)
        {
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsWalking", true);
            GameObject.Find("Player").transform.position += new Vector3(1f, 0f) * Time.deltaTime * GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed;
        }
        else
        {
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsWalking", false);
            Invoke("NextLevel", 1f);
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
