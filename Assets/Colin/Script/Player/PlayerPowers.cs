using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public GameObject waterBulletPrefab;
    private int bulletDirection;

    public void Grow()
    {
        // Change Sprite and allow for water shoot to spawn in
    }

    public void Shrink()
    {
        // Change Sprite to small and allow grow to spanw in
    }

    void WaterPower()
    {
        if (gameObject.GetComponent<PlayerMovement>().playerDirection)
        {
            bulletDirection = 1;
        }
        else
        {
            bulletDirection = -1;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(waterBulletPrefab, transform.position + new Vector3(0.5f * bulletDirection, 0.5f, 0), Quaternion.identity);
        }
    }
}
