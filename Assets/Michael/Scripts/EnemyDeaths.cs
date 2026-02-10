using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour

{

    [SerializeField] SpriteRenderer enemySprite;
    public Sprite deathSprite;
    public bool isDead;
    public bool dead;

    void Update()
    {
        if(isDead)
        {
            GetShot();
            Destroy(this.gameObject, 3);
        }
    }

    public void Squish()
    {
        dead = true;
        enemySprite.sprite = deathSprite;
        Destroy(this.gameObject, 1);
    }

    public void GetShot()
    {
        dead = true;
        transform.position += new Vector3(0f, -1f) * Time.deltaTime * 3f;
    }

}
