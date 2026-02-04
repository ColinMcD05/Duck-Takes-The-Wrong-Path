using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public GameObject waterBulletPrefab;
    public int bulletDirection;
    public int amountOfWater;
    [SerializeField] private PlayerController playerController;

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

        // if press q, Quack
    }

    public void Shrink()
    {
        // Change Sprite to small and allow grow to spawn in
        if (playerController.currentPower == "Water")
        {
            playerController.currentPower = "Grow";
            Debug.Log(playerController.currentPower);
        }
        else if (playerController.currentPower == "Grow")
        {
            playerController.currentPower = "Small";
            Debug.Log(playerController.currentPower);
        }
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
