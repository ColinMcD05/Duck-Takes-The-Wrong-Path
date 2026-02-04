using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public GameObject waterBulletPrefab;
    public int bulletDirection;
    public int amountOfWater;

    private void Update()
    {
        if (gameObject.GetComponent<PlayerController>().currentPower == "Water")
        {
            WaterPower();
        }
    }

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
            bulletDirection = -1;
        }
        else
        {
            bulletDirection = 1;
        }
        if (Input.GetButtonDown("Fire1") && amountOfWater < 2 )
        {
            Instantiate(waterBulletPrefab, transform.position + new Vector3(0.65f * bulletDirection, 0.12f, 0), Quaternion.identity);
            amountOfWater += 1;
        }
    }
}
