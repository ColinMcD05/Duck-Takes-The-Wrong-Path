using UnityEngine;

public class ChestCrate : MonoBehaviour
{
    public float itemType;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject wormPrefab;
    [SerializeField] GameObject waterPrefab;

    public void SpawnItem()
    {
        string playerCurrentPower = playerController.currentPower;
    }

    public void MoveChest()
    {

    }

    public void Break()
    {
        Destroy(this.gameObject);
    }
}
