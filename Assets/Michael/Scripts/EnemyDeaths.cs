using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour

{

    [SerializeField] SpriteRenderer enemySprite;
    public Sprite deathSprite;
    public bool dead;
    public int direction;
    public bool canSpawn;

    void Update()
    {
    }

    public void Squish()
    {
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        dead = true;
        enemySprite.sprite = deathSprite;
        Destroy(this.gameObject, 1);
    }

    public void GetShot()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        dead = true;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(4 * direction, 3f);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Destroy(this.gameObject, 2f);
    }
}
