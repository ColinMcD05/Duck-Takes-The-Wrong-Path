using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwo : MonoBehaviour
{


    [SerializeField] GameObject boss;
    private bool levelEnd;
    private float time;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (levelEnd)
        {
            time += Time.deltaTime;
            if (time >= 0.2f)
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
            boss.GetComponent<BossController>().canMove = false;
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
        if (GameObject.Find("Player").transform.position.x < 204.62f)
        {
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsWalking", true);
            GameObject.Find("Player").transform.position += new Vector3(1f, 0f) * Time.deltaTime * GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed;
            GameObject.Find("MainCamera").transform.position += new Vector3(1f, 0f) * Time.deltaTime * GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed;
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
