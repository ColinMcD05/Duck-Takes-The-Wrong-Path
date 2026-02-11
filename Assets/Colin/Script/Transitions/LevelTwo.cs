using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwo : MonoBehaviour
{


    [SerializeField] GameObject boss;
    private bool levelEnd;
    private float time;
    private GameManager gameManager;
    private GameObject player;
    private Camera mainCamera;
    [SerializeField] AudioClip levelEndAudio;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (levelEnd)
        {
            time += Time.deltaTime;
            if (time >= 1f)
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
            if (boss != null)
            {
                boss.GetComponent<BossController>().canMove = false;
            }
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
                Destroy(GameObject.Find("BreakableFloor"));
            }
            Destroy(GameObject.Find("Enemies"));
            // Invoke("NextLevel", 5f);
        }
    }

    void TransitionMovement()
    {
        if (player.transform.position.x < 204.62f)
        {
            player.GetComponent<Animator>().SetBool("IsWalking", true);
            player.transform.position += new Vector3(1f, 0f) * Time.deltaTime * GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed;
            mainCamera.transform.position += new Vector3(1f, 0f) * Time.deltaTime * GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed;
        }
        else
        {
            player.GetComponent<Animator>().SetBool("IsWalking", false);
            Invoke("NextLevel", 1f);
        }
    }

    void BossMovement()
    {
        if (boss != null)
        {
            boss.transform.position -= new Vector3(0f, 1f) *Time.deltaTime * 10;
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
