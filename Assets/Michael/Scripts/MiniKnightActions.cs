using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MiniKnightActions : MonoBehaviour
{
    private bool canMove;
    public float speed = 2f;
    public int direction = -1;
    public int moveDirection = -1;
    public float wanderRange = 5f;
    private static GameObject player;
    [SerializeField] private Rigidbody2D mkb;
    [SerializeField] private SpriteRenderer mks;
    public GameObject axePrefab;
    public Transform axePos;
    public float timer;
    public float moveTimer;
    public float attackTimer = 1f;


    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mkb = GetComponent<Rigidbody2D>();
        mks = GetComponent<SpriteRenderer>();
        mks.flipX = direction == -1 ? false : true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove && !gameObject.GetComponent<EnemyDeath>().dead && !player.GetComponent<PlayerDeath>().dead)
        {
            Movement();
            axeThrow();
            ChangeDirection();
            gameObject.GetComponent<Animator>().enabled = true;
        }    

        else
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
        
    }

    void axeThrow()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = Random.Range(1f, 3f);
            GameObject axe = Instantiate(axePrefab, new Vector3(transform.position.x - mks.bounds.extents.x * direction, transform.position.y), Quaternion.Euler(0f, 0f, -45f * direction));
            axe.GetComponent<AxeMovement>().axeDirection = direction;
            axe.GetComponent<SpriteRenderer>().flipX = mks.flipX;
        }
        this.gameObject.GetComponent<Animator>().SetTrigger("Throw");
    }

    void ChangeDirection()
    {
        if (CheckDirection() > 0)
        {
            mks.flipX = true;
            direction = 1;
        }
        else
        {
            mks.flipX = false;
            direction = -1;
        }
    }

    float CheckDirection()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 toOther = Vector3.Normalize(player.transform.position - transform.position);
        return Vector3.Dot(forward, toOther);
    }

    void Movement()
    {
        if (moveTimer <= 0)
        {
            moveTimer = 0.75f;
            moveDirection *= -1;
        }
        else
        {
            mkb.position += new Vector2(0.5f, 0f) * Time.deltaTime * speed * moveDirection;
            moveTimer -= Time.deltaTime;
        }
    }
    
         private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            direction *= -1;
            mks.flipX = !mks.flipX;
        }
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }

}