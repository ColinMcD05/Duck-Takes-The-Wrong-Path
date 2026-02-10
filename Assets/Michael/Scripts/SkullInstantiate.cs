using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkullInstantiate : MonoBehaviour
{
    [SerializeField] GameObject skullPrefab;
    bool spawned;

    private void Update()
    {
        if (gameObject.GetComponent<EnemyDeath>().canSpawn && !spawned)
        {
            Invoke("CreateSkull", 0.99f);
            spawned = true;
        }
    }

    void CreateSkull()
    {
        Instantiate(skullPrefab, new Vector2(transform.position.x, transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y), Quaternion.identity);
    }
}
