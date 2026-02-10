using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoblinDeath : MonoBehaviour

{
    [SerializeField] SpriteRenderer goblinSprite;
    public Sprite deathSprite;
    public bool isDead;

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
        goblinSprite.sprite = deathSprite;
        Destroy(this.gameObject, 1);
    }

    public void GetShot()
    {
        transform.position += new Vector3(0f, -1f) * Time.deltaTime * 3f;
    }


}
