using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public bool isDead;
    void Update()
    {
       if (isDead)
        {
            DeathMovement();
            Destroy(this.gameObject, 3f);
        }
    }

    void DeathMovement()
    {
        transform.position += new Vector3(0f, -1f) * Time.deltaTime * 3f;
    }
}
